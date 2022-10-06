﻿using Supabase.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Supabase.Client;

namespace Supabase.Service
{
    public class SupaBaseService
    {
        public async void SupaBaseClientConnection()
        {
            //var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
            //var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");
            var url = "https://ttyrdrkblkibpsiaqlyp.supabase.co";
            var key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InR0eXJkcmtibGtpYnBzaWFxbHlwIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTY2NDg5MDM4OCwiZXhwIjoxOTgwNDY2Mzg4fQ.Y4kb_hLVihtsnxe76G_MwU8OcXa1VD4Speq-FlBvMfQ";

            await Supabase.Client.InitializeAsync(url, key);
        }

        public async void SupaBaseCompanyRun()
        {
            var instance = Supabase.Client.Instance;

            var allCompanies = await instance.From<Company>().Get();

            List<Company> myCompanies = new List<Company>(
                allCompanies.Models.ToList());

            var temp = 3;
        }
        public async void SupaBaseCompanyAdminRun()
        {
            var instance = Supabase.Client.Instance;

            var allCompanyAdmins = await instance.From<CompanyAdmin>().Get();

            List<CompanyAdmin> myCompanyAdmins = new List<CompanyAdmin>(
                allCompanyAdmins.Models.ToList());

            var temp = 2;
        }

        public async void SupaBaseEmployeeRun()
        {
            var instance = Supabase.Client.Instance;

            // Access Postgrest using:
            var allEmployees = await instance.From<Employee>().Get();
            
            List<Employee> employeesToSync = new List<Employee>(
                allEmployees.Models.Where(c => c.sync == true).ToList());
            
            var temp = 3;
        }


    }
}