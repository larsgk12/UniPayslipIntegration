using Economy;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Newtonsoft.Json;
using Supabase.Models;
using UniPayslipIntegration.SoftrigModels;
using System.Collections.Generic;
using SupabaseConnection.SupaBaseModels;

namespace Softrig;

public interface IUniDataService
{
    Task<List<String>> GetPayslips(int payrollRunId, List<int> employees, string companyKey);
    Task<List<SupaBaseEmployee>> GetEmployees(string companyKey);
    void FetchCompanies();

}

public class UniDataService : IUniDataService
{
    private Api _api;
    JsonSerializerOptions _serializerOptions;

    public List<Company> Companies { get; set; } = new List<Company>();

    public UniDataService()
    {
        _api = new Api();
        _serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };


    }

    public void InitSoftRigApi(string softrigUrl, string authUrl, string password, string clientId)
    {
        _api = new Api(softrigUrl, authUrl);
        var certificate = new X509Certificate2(@"c:\temp\SoftRigCert_39034aee-5e6a-41b7-b464-6a263e0c0205.p12", password);
        if (_api.ServerLogin(clientId, "AppFramework Payroll.Admin", certificate).Result)
        {
            Companies = _api.GetCompanies().Result;
            foreach (var comp in Companies)
            {
                Console.WriteLine($"Companies to sync: {comp.Name}");
            }
        }
    }


    public async void FetchCompanies()
    {
        Companies = await _api.GetCompanies();
        foreach (var comp in Companies)
        {
            Console.WriteLine($"Companies to sync: {comp.Name}");
        }
    }

    public List<SupaBasePayroll> GetAllPayslips(List<int> employees, string companyKey)
    {
        _api.CompanyKey = companyKey;
        var payrollruns = GetParollRun(companyKey);
        var stringofEmp = string.Join(",", employees.Select(n => n.ToString()).ToArray());
        var supabasePayroll = new List<SupaBasePayroll>();
        if (payrollruns != null)
        {
            foreach (var run in payrollruns) //Fetch payslips for every emp in company
            {
                string url = $"api/biz/paycheck?action=inselection&payrollID={run.ID}&employees={stringofEmp}";
                var payslips = _api.Get(url).Result;
                // MAtch result to SupabasePayroll 
                Console.WriteLine("Fetched payslips");

            }

        }
        return null;
    }

    public async Task<List<string>> GetPayslips(int payrollRunId, List<int> employees, string companyKey)
    {
        string url = $"api/biz/paycheck?action=inselection&payrollID={payrollRunId}";
        List<string> payslips = new List<string>();
        _api.CompanyKey = companyKey;

        await _api.Get(url);
        if (_api.LastResult != null)
        {
            var test = _api.LastResult;
        }
        return payslips;
    }

    public async Task<List<SupaBaseEmployee>> GetEmployees(string companyKey)
    {
        _api.CompanyKey = companyKey;
        string url = $"api/statistics?model=employee&expand=BusinessRelationInfo.DefaultEmail&select=ID as ID,BusinessRelationInfo.Name as Name,DefaultEmail.EmailAddress as email";
        var empJson = await _api.Get<EmployeeStatistics>(url);
        return empJson.Data;
    }

    private List<PayrollRun> GetParollRun(string companyKey)
    {
        _api.CompanyKey = companyKey;
        string url = $"biz/PayrollRun?filter=StatusCode > 1";
        var payrollRuns = _api.Get<List<PayrollRun>>(url).Result;
        return payrollRuns;

    }

}

