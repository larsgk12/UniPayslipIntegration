using Microsoft.Extensions.Options;
using Softrig;
using Supabase.Models;
using Supabase.Service;
using SupabaseConnection.Service;

namespace UniPayslipIntegration
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly SoftRigSettings _softRigSettings;
        private readonly SupabaseSettings _supabaseSettings;

        public Worker(ILogger<Worker> logger, IOptions<SoftRigSettings> softRigSettings, IOptions<SupabaseSettings> supabaseSettings)
        {
            _logger = logger;
            _softRigSettings = softRigSettings.Value;
            _supabaseSettings = supabaseSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var supaBase = new SupaBaseService();
            supaBase.SupaBaseClientConnection(_supabaseSettings.key, _supabaseSettings.url);

            var supabaseCompanies = new CompanyCtrl();
            var employeeRun = new EmployeeCtrl();
            var supabasePayroll = new PayrollCtrl();

            var uniDataService = new UniDataService();
            uniDataService.InitSoftRigApi(_softRigSettings.softrigUrl, _softRigSettings.authUrl, _softRigSettings.certificatePassword, _softRigSettings.clientID);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                if (uniDataService.Companies.Count > 0)
                {
                    var uniCompanies = uniDataService.Companies;
                    var supaBasecompanies = supabaseCompanies.GetSupaBaseCompany().Result;

                    var listOfNewcompanies = uniCompanies.Where(c => supaBasecompanies.All(s => c.Key != s.Companykey)).ToList();
                    if (listOfNewcompanies.Count > 0) // New companies to sync
                    {
                        var supabaseComp = listOfNewcompanies.Select(c => new SupaBaseCompany { Companykey = c.Key, Name = c.Name }).ToList();
                        supabaseCompanies.PostSupaBaseCompany(supabaseComp);
                        var allSupbaseCompanies = supabaseCompanies.GetSupaBaseCompany().Result;
                        foreach (var newComp in listOfNewcompanies)
                        {
                            var supabasecomp = allSupbaseCompanies.Where(c => c.Companykey == newComp.Key).FirstOrDefault();
                            if (supabasecomp != null)
                            {
                                var employee = await uniDataService.GetEmployees(newComp.Key);
                                if (employee != null)
                                {
                                    //Append supabasecompany
                                    employee.ForEach(c => c.SupaBaseCompanyID = supabasecomp.id);
                                    employeeRun.PostSupaBaseEmployee(employee);
                                }
                            }
                        }
                    }

                    //Sync payslips fire
                    //TODO only send not sendt earlier
                    var employeeWithSync = employeeRun.GetSupaBaseEmployeeToSync().Result;
                    var payslippInSupabase = supabasePayroll.GetSupaBasePayroll().Result;
                    var employeesSyncedBefore = payslippInSupabase.GroupBy(g => g.EmployeeID).Select(g => g.Key);

                    var employeeForPayslippSync = employeeWithSync.Where(e => employeesSyncedBefore.All(s => s != e.ID)).ToList();

                    if (employeeForPayslippSync != null)
                    {
                        var companies = employeeForPayslippSync.GroupBy(e => e.SupaBaseCompanyID).Select(g => g.Key);
                        foreach (var company in companies)
                        {
                            var supabaseCompany = supaBasecompanies.Where(c => c.id == company).First();
                            var payslips = uniDataService.GetAllPayslips(employeeForPayslippSync.Where(e => e.SupaBaseCompanyID == company).ToList(), supabaseCompany.Companykey);
                            if (payslips.Count > 0) { 
                                supabasePayroll.PostSupaBasePayroll(payslips);
                                Console.WriteLine($"Added new payslips:{payslips.Count}");
                            }
                        }

                    }
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }


}