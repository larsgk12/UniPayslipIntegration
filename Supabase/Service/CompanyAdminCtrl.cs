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
        public async Task<List<SupaBaseCompanyAdmin>> GetSupaBaseCompanyAdmin()
        {
            var instance = Supabase.Client.Instance;

            var allCompanyAdmins = await instance.From<SupaBaseCompanyAdmin>().Get();

            List<SupaBaseCompanyAdmin> myCompanyAdmins = new List<SupaBaseCompanyAdmin>(
                allCompanyAdmins.Models.ToList());

            return myCompanyAdmins;
        }

        public void PostSupaBaseCompanyAdmin(List<SupaBaseCompanyAdmin> CompanyAdmins)
        {
            foreach (var CompanyAdmin in CompanyAdmins)
            {
                var instance = Supabase.Client.Instance;
                var insert = instance.From<SupaBaseCompanyAdmin>().Insert(CompanyAdmins);
            }
        }
    }
}
