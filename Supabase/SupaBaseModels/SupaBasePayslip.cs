using Postgrest.Attributes;
using Supabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupabaseConnection.SupaBaseModels
{
    [Table("payslip")]
    public class SupaBasePayroll : SupabaseModel
    {
        [Column("email")]
        public string EmployeeEmail { get; set; }
        [Column("date")]
        public string Date { get; set; }
        [Column("data")]
        public object Data { get; set; }
        [Column("external_employee_id")]
        public int? EmployeeID { get; set; }
        [Column("external_payrollrun_id")]
        public int? PayrollRunID { get; set; }

        //SupaBaseColumns
        [PrimaryKey("id", false)]
        public int SupaBaseID { get; set; }
        [Column("company_id")]
        public int SupaBaseCompanyID { get; set; }

    }

    public class SupaBasePayrollRun
    {
        public List<SupaBasePayroll> Payroll { get; set; }
    }

}
