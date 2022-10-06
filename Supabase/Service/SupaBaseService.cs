using Supabase.Models;
using Supabase.Functions;
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
        public async void SupaBaseClientConnection(string key, string url)
        {
            await Supabase.Client.InitializeAsync(url, key);
        }
    }
}
