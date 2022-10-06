using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supabase.Models;

[Table("company_admin")]
public class CompanyAdmin : SupabaseModel
{
    [PrimaryKey("id", false)]
    public int id { get; set; }
    public string company_id { get; set; }
    public string email { get; set; }
    public string created_at { get; set; }
}
