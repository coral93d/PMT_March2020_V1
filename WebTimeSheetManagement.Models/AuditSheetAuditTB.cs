using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    [Table("QA_Audit_Acceptance")]
    public class AuditSheetAuditTB
    {
        [Key]
        public int Audit_AcceptanceID { get; set; }
        public int Audit_MasterID { get; set; }

        public string UserName { get; set; }
        public int Auditor_ID { get; set; }
        public DateTime? Audit_Date { get; set; }
        public DateTime? Created_On { get; set; }
        public string User_Comments { get; set; }
        public int Acceptance_Status { get; set; }
        //   public int TimeSheetID { get; set; }
        //  public int UserID { get; set; }
    }

    /**    [Table("ExpenseAuditTB")]
      public class ExpenseAuditTB
       {
           [Key]
           public int ApprovaExpenselLogID { get; set; }
           public int ApprovalUser { get; set; }
           public DateTime? ProcessedDate { get; set; }
           public DateTime? CreatedOn { get; set; }
           public string Comment { get; set; }
           public int Status { get; set; }
           public int ExpenseID { get; set; }
           public int UserID { get; set; }
       }

       **/

}
