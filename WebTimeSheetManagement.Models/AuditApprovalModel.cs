using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
  
    public class AuditSheetApproval
    {
        public int Audit_MasterID { get; set; }
        public string User_Comments { get; set; }
    }

    public class AuditSheetAppeal
    {

        public int Audit_MasterID { get; set; }
        public string Auditor_Comments { get; set; }

    }
}
