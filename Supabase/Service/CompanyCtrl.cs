using Supabase.Models;
using SupabaseConnection.SoftRigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupabaseConnection.Service
{
    public class CompanyCtrl
    {
        public async Task<List<Company>> GetSupaBaseCompany()
        {
            var instance = Supabase.Client.Instance;

            var allCompanies = await instance.From<Company>().Get();

            List<Company> myCompanies = new List<Company>(
                allCompanies.Models.ToList());

            return myCompanies;
        }

        public async void PostSupaBaseCompany(List<SoftRigCompany> softRigCompanies)
        {
            foreach (var softRigCompany in softRigCompanies)
            {
                var model = new Supabase.Models.Company
                {
                    name = softRigCompany.Name,
                    external_id = softRigCompany.id,
                    companykey = softRigCompany.CompanyKey,
                };

                var instance = Supabase.Client.Instance;
                var insert = await instance.From<Company>().Insert(model);
            }
        }
    }
}
