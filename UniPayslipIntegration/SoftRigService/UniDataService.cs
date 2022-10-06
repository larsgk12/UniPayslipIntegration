using Economy;
using System.Security.Cryptography.X509Certificates;
using SupabaseConnection.SoftRigModels;
using System.Text.Json;
using Newtonsoft.Json;

namespace Softrig;

public interface IUniDataService
{
    Task<List<String>> GetPayslips(int payrollRunId, List<int> employees, string companyKey);
    Task<List<String>> GetPayrollruns(List<int> employees, string companyKey);

    Task<List<SoftRigEmployee>> GetEmployees(string companyKey);
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

    public async void InitSoftRigApi(string softrigUrl, string authUrl, string password, string clientId)
    {
        _api = new Api(softrigUrl, authUrl);
        var certificate = new X509Certificate2(@"c:\temp\SoftRigCert_39034aee-5e6a-41b7-b464-6a263e0c0205.p12", password);
        await _api.ServerLogin(clientId, "AppFramework Payroll.Admin", certificate);
        Console.WriteLine("Logged into server");
        Companies = await _api.GetCompanies();
        foreach (var comp in Companies)
        {
            Console.WriteLine($"Companies to sync: {comp.Name}");
        }
    }

    public Task<List<string>> GetPayrollruns(List<int> employees, string companyKey)
    {
        throw new NotImplementedException();
    }

    public async void FetchCompanies()
    {
        Companies = await _api.GetCompanies();
        foreach (var comp in Companies)
        {
            Console.WriteLine($"Companies to sync: {comp.Name}");
        }
    }

    public async Task<List<string>> GetPayslips(int payrollRunId, List<int> employees, string companyKey)
    {
        string url = $"api/biz/paycheck?action=inselection&payrollID={payrollRunId}";
        List<string> payslips = new List<string>();
        _api.AddCustomerHeader("companyKey", companyKey);

        await _api.Get(url);
        if (_api.LastResult != null)
        {
            var test = _api.LastResult;
        }
        return payslips;
    }

    public async Task<List<SoftRigEmployee>> GetEmployees(string companyKey)
    {
        _api.CompanyKey = companyKey;
        string url = $"api/statistics?model=employee&expand=BusinessRelationInfo.DefaultEmail&select=ID as ID,BusinessRelationInfo.Name as Name,DefaultEmail.EmailAddress as email";
        var empJson = await _api.Get<EmployeeStatistics>(url);
        return empJson.Data;



    }
}

