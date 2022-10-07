using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupabaseConnection.SoftRigModels
{
    public class SoftRigEmployee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public int companyID { get; set; }
    }

}
