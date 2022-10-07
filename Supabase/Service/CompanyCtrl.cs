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

        public void PostSupaBaseCompany(List<Company> Companies)
        {
                var instance = Supabase.Client.Instance;
                var insert = instance.From<Company>().Insert(Companies);
        }
    }
}
