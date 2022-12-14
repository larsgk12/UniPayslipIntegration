using Supabase.Models;

namespace SupabaseConnection.Service
{
    public class CompanyAdminCtrl
    {
        public async Task<List<SupaBaseCompanyAdmin>> GetSupaBaseCompanyAdmin()
        {
            var instance = Supabase.Client.Instance;

            var CompanyAdmins = await instance.From<SupaBaseCompanyAdmin>().Get();

            var allCompanyAdmins = CompanyAdmins.Models.ToList();

            return allCompanyAdmins;
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
