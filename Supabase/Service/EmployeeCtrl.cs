using Supabase.Models;
using Postgrest.Attributes;
using SupabaseConnection.SoftRigModels;
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

        public async Task<List<Employee>> GetSupaBaseEmployeeAll()
        {
            var instance = Supabase.Client.Instance;

            var Employees = await instance.From<Employee>().Get();
            var allEmployees = Employees.Models.ToList();

            return allEmployees;
        }
        public async Task<List<Employee>> GetSupaBaseEmployeeToSync()
        {
            var instance = Supabase.Client.Instance;

            var allEmployees = await instance.From<Employee>().Get();

            var employeesToSync = allEmployees.Models.Where(c => c.sync == true).ToList();
            
            return employeesToSync;
        }

        public void PostSupaBaseEmployee(List<Employee> employees)
        {
                var instance = Supabase.Client.Instance;
                var insert = instance.From<Employee>().Insert(employees);
        }
    }
}
