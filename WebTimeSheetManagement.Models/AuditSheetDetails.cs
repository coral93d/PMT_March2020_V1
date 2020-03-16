using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTimeSheetManagement.Models
{
    [Table("QA_Audit_Details")]
    public class AuditSheetDetails
    {
        [Key]
        public int Audit_ID { get; set; }
        public int Audit_MasterID { get; set; }
        public int FormsCode { get; set; }
       // public string FormName { get; set; }
        public int FieldID { get; set; }
        public string Questions { get; set; }
        public bool Parameter_Status { get; set; }
        public string Metric { get; set; }
        public int Max_Score { get; set; }
        public int Final_Score { get; set; }
      
       
    }
}
