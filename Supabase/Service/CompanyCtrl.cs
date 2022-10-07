using Supabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupabaseConnection.Service
{
    public class CompanyCtrl
    {
        public async Task<List<SupaBaseCompany>> GetSupaBaseCompany()
        {
            var instance = Supabase.Client.Instance;

            var Companies = await instance.From<SupaBaseCompany>().Get();

            var allCompanies = Companies.Models.ToList();

            return allCompanies;
        }

        public void PostSupaBaseCompany(List<SupaBaseCompany> Companies)
        {
                var instance = Supabase.Client.Instance;
                var insert = instance.From<SupaBaseCompany>().Insert(Companies);
        }
    }
}
