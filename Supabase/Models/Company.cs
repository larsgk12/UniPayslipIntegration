using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supabase.Models;
[Table("company")]
public class Company : SupabaseModel
{
    [PrimaryKey("id", false)]
    public int id { get; set; }
    public string key { get; set; }
    public string name { get; set; }
    public string created_at { get; set; }
}
