using Postgrest.Attributes;
using SupabaseConnection.SoftRigModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supabase.Models;

[Table("company_admin")]
public class SupaBaseCompanyAdmin : SupabaseModel
{
    [PrimaryKey("id", false)]
    public int id { get; set; }
    public string name { get; set; }
    [Column("company_id")]
    public int company_id { get; set; }
    [Column("email")]
    public string Email { get; set; }
}

public class CompanyAdminStatistics
{
    public List<SupaBaseCompanyAdmin> Data { get; set; }
}
