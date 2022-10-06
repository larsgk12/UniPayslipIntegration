using Economy;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UniPayslipIntegration;
using System.Reflection.Metadata;
using System.Security.Policy;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;

namespace Softrig;

public interface IUniDataService
{
    Task<List<String>> GetPayslips(int payrollRunId, List<int> employees, string companyKey);
    Task<List<String>> GetPayrollruns(List<int> employees, string companyKey);
    void FetchCompanies();

}

public class UniDataService : IUniDataService
{
    private Api _api;

    public List<Company> Companies { get; set; }

    public UniDataService()
    {
        _api = new Api();

    }

    public async void InitSoftRigApi(string softrigUrl, string authUrl, string password, string clientId)
    {

        _api = new Api(softrigUrl, authUrl);
        var certificate = new X509Certificate2(@"c:\temp\SoftRigCert_39034aee-5e6a-41b7-b464-6a263e0c0205.p12", password);
        await _api.ServerLogin(clientId, "AppFramework Payroll.Admin", certificate);
        Console.WriteLine("Logged into server");
        FetchCompanies();

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

}

