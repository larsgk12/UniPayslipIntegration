using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniPayslipIntegration
{
    public class SoftRigSettings
    {
        public string authUrl { get; set; }
        public string softrigUrl { get; set; }
        public string certificatePassword { get; set; }
        public string clientID { get; set; }

    }
    public class SupabaseSettings
    {
        public string url { get; set; }
        public string key { get; set; }

    }
}
