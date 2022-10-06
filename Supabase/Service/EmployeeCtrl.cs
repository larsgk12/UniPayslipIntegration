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
        public async Task<List<Employee>> GetSupaBaseEmployee()
        {
            var instance = Supabase.Client.Instance;

            // Access Postgrest using:
            var allEmployees = await instance.From<Employee>().Get();

            List<Employee> employeesToSync = new List<Employee>(
                allEmployees.Models.Where(c => c.sync == true).ToList());
            
            return employeesToSync;
        }

        public async void PostSupaBaseEmployee(List<SoftRigEmployee> softRigEmployees)
        {
            foreach (var softRigEmployee in softRigEmployees)
            {
                var model = new Supabase.Models.Employee
                {
                    name = softRigEmployee.Name,
                    external_id = softRigEmployee.ID,
                    company_id = 3
                };

                var instance = Supabase.Client.Instance;
                var insert = await instance.From<Employee>().Insert(model);
                Console.WriteLine("Employee inserted" + softRigEmployee.Name);
            }
        }
    }
}
