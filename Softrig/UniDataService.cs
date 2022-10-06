using Economy;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Softrig;

public interface IUniDataService
{
    Task<List<String>> GetPayslips(List<int> employees, string companyKey);

}


public class UniDataService : IUniDataService
{

    private string _baseUrl = "https://dev.unieconomy.no";
    private RequestProvider _requestProvider;
    private Api _api;

    public UniDataService(RequestProvider requestProvider)
    {
        _api = new Api("https://test-api.softrig.com/", "https://test-login.softrig.com/");
        var certificate = new X509Certificate2(@"SoftRigCert_39034aee-5e6a-41b7-b464-6a263e0c0205.p12", "{password}");
        _api.ServerLogin("", "", certificate);
        _requestProvider = requestProvider;
    }

    public async Task<List<string>> GetPayslips(List<int> employees, string companyKey)
    {
        string url = "";
        List<string> payslips = new List<string>();

        try
        {
            payslips = await _requestProvider.GetAsync<List<string>>(url, "", "").ConfigureAwait(false);
        }
        catch (HttpRequestExceptionEx exception) when (exception.HttpCode == System.Net.HttpStatusCode.NotFound)
        {
            payslips = new List<string>();
        }
        return payslips;
    }
}

