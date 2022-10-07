using Postgrest.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupabaseConnection.SupaBaseModels
{
    public class SupaBasePayroll
    {
        [Column("email")]
        public string EmployeeEmail { get; set; }
        public string Date { get; set; }
        public string Data { get; set; }
        [Column("external_employee_id")]
        public int EmployeeID { get; set; }
        [Column("external_payrollrun_id")]
        public int PayrollRunID { get; set; }

        //SupaBaseColumns
        [PrimaryKey("id")]
        public int SupaBaseID { get; set; }
        [Column("company_id")]
        public int SupaBaseCompanyID { get; set; }

    }

    public class SupaBasePayrollRun
    {
        public List<SupaBasePayroll> Payroll { get; set; }
    }

}
