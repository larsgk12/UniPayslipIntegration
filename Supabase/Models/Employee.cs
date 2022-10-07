using Postgrest.Attributes;
using System.ComponentModel;

namespace Supabase.Models;

[Table("employee")]
public class Employee : SupabaseModel
{

    [PrimaryKey("id", false)]
    public int Id { get; set; }
    [Column("company_id")]
    public int companyID { get; set; }
    public int external_id  { get; set; }
    public bool sync { get; set; }
    [Column("name")]
    public string Name { get; set; }
}

public class EmployeeStatistics
{
    public List<Employee> Data { get; set; }
}

