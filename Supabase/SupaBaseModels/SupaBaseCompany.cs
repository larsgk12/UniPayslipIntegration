using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supabase.Models;
[Table("company")]
public class SupaBaseCompany : SupabaseModel
{
    [PrimaryKey("id", false)]
    public int id { get; set; }
    [Column("key")]
    public string Companykey { get; set; }
    [Column("name")]
    public string Name { get; set; }

}
