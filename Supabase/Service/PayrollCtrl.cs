using Supabase.Models;
using SupabaseConnection.SupaBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupabaseConnection.Service
{
    public class PayrollCtrl
    {
        public async Task<List<SupaBasePayroll>> GetSupaBasePayroll()
        {
            var instance = Supabase.Client.Instance;

            var payroll = await instance.From<SupaBasePayroll>().Get();
            var allPayroll = payroll.Models.ToList();

            return allPayroll;
        }

        public void PostSupaBasePayroll(List<SupaBasePayroll> payrolls)
        {
                var instance = Supabase.Client.Instance;
                var insert = instance.From<SupaBasePayroll>().Insert(payrolls);
        }
    }
}
