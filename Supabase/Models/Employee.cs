using Postgrest.Attributes;
using System.ComponentModel;

namespace Supabase.Models;

[Table("employee")]
public class Employee : SupabaseModel
{

    [PrimaryKey("id", false)]
    public int Id { get; set; }
    public int company_id { get; set; }
    public int external_id  { get; set; }
    public bool sync { get; set; } 
    public string name { get; set; }
}