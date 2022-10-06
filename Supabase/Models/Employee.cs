using Postgrest.Attributes;

namespace Supabase.Models;

[Table("employee")]
public class Employee : SupabaseModel
{

    [PrimaryKey("id", false)]
    public int Id { get; set; }

}