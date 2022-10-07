using Postgrest.Attributes;
using System.ComponentModel;

namespace Supabase.Models;

[Table("employee")]
public class SupaBaseEmployee : SupabaseModel
{
    [Column("external_id")]
    public int ID  { get; set; }
    [Column("name")]
    public string Name { get; set; }
    
    //SupaBaseFields
    [PrimaryKey("id", false)]
    public int SupaBaseId { get; set; }
    [Column("company_id")]
    public int SupaBaseCompanyID { get; set; }
    public bool SupaBasesync { get; set; }
}

public class EmployeeStatistics
{
    public List<SupaBaseEmployee> Data { get; set; }
}

