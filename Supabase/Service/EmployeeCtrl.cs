using Supabase.Models;
using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Postgrest;
using System.ComponentModel;

namespace SupabaseConnection.Service
{
    public class EmployeeCtrl
    {

        public async Task<List<SupaBaseEmployee>> GetSupaBaseEmployeeAll()
        {
            var instance = Supabase.Client.Instance;

            var Employees = await instance.From<SupaBaseEmployee>().Get();
            var allEmployees = Employees.Models.ToList();

            return allEmployees;
        }
        public async Task<List<SupaBaseEmployee>> GetSupaBaseEmployeeToSync()
        {
            var instance = Supabase.Client.Instance;

            var allEmployees = await instance.From<SupaBaseEmployee>().Get();

            var employeesToSync = allEmployees.Models.Where(c => c.SupaBasesync == true).ToList();
            
            return employeesToSync;
        }

        public void PostSupaBaseEmployee(List<SupaBaseEmployee> employees)
        {
            var instance = Supabase.Client.Instance;
            var insert = instance.From<SupaBaseEmployee>().Insert(employees);
        }
    }
}
