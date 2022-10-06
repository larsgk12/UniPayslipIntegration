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

        public async void PostSupaBaseCompanyAdmin(List<SoftRigCompanyAdmin> softRigCompanyAdmins)
        {
            foreach (var softRigCompanyAdmin in softRigCompanyAdmins)
            {
                var model = new Supabase.Models.CompanyAdmin
                {
                   name = softRigCompanyAdmin.name,
                   email = softRigCompanyAdmin.Email,
                   company_id = softRigCompanyAdmin.CompanyKey,
                };

                var instance = Supabase.Client.Instance;
                var insert = await instance.From<CompanyAdmin>().Insert(model);
            }
        }
    }
}
