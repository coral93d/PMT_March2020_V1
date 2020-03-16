using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    [NotMapped]
    public class AuditSheetMasterView
    {
        public int Audit_MasterID { get; set; }
        public string Audit_Date { get; set; }
        public string Transaction_Date { get; set; }
        public string Invoice_Number { get; set; }
        public string Date_Of_Service { get; set; }
        public string Audit_Type { get; set; }
        public string Marks_scored { get; set; }
        public string Final_Marks { get; set; }

        public string Result { get; set; }

        public int? Auditor_ID { get; set; }
        public int UserID { get; set; }
        //  public string CreatedOn { get; set; }
        public string Username { get; set; }
        public string Auditor_Name { get; set; }
        // public string SubmittedMonth { get; set; }
        public string AuditSheetStatus { get; set; }
        public string User_Comments { get; set; }
        public string Coaching_Comments { get; set; }
        public string Acceptance_Status { get; set; }
        public string Appeal_Status { get; set; }



    }
}
