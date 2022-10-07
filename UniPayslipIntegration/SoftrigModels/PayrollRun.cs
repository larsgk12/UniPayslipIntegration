using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniPayslipIntegration.SoftrigModels;

public class PayrollRun
{
    public int ID { get; set; }
    public DateTime PayDate { get; set; }
    public string Description { get; set; } = default!;

}
