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

            var uniDataService = new UniDataService();
            uniDataService.InitSoftRigApi(_softRigSettings.softrigUrl, _softRigSettings.authUrl, _softRigSettings.certificatePassword, _softRigSettings.clientID);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                if (uniDataService.Companies.Count > 0)
                {
                    var uniCompanies = uniDataService.Companies;
                    var comp = await supabaseCompanies.GetSupaBaseCompany();

                    var listOfNewcompanies = uniCompanies.Where(c => comp.All(s => c.Key != s.Companykey)).ToList();
                    if (listOfNewcompanies.Count > 0)
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
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }


}