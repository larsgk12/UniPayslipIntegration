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
        public async Task<List<SupaBaseCompany>> GetSupaBaseCompany()
        {
            var instance = Supabase.Client.Instance;

            var allCompanies = await instance.From<SupaBaseCompany>().Get();

            List<SupaBaseCompany> myCompanies = new List<SupaBaseCompany>(
                allCompanies.Models.ToList());

            return myCompanies;
        }

        public void PostSupaBaseCompany(List<SupaBaseCompany> Companies)
        {
                var instance = Supabase.Client.Instance;
                var insert = instance.From<SupaBaseCompany>().Insert(Companies);
        }
    }
}
