using Supabase.Models;
using SupabaseConnection.SoftRigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupabaseConnection.Service
{
    public class CompanyAdminCtrl
    {
        public async Task<List<CompanyAdmin>> GetSupaBaseCompanyAdmin()
        {
            var instance = Supabase.Client.Instance;

            var allCompanyAdmins = await instance.From<CompanyAdmin>().Get();

            List<CompanyAdmin> myCompanyAdmins = new List<CompanyAdmin>(
                allCompanyAdmins.Models.ToList());

            return myCompanyAdmins;
        }

        public void PostSupaBaseCompanyAdmin(List<CompanyAdmin> CompanyAdmins)
        {
            foreach (var CompanyAdmin in CompanyAdmins)
            {
                var instance = Supabase.Client.Instance;
                var insert = instance.From<CompanyAdmin>().Insert(CompanyAdmins);
            }
        }
    }
}
