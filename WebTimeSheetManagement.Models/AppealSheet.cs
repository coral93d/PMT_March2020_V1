using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTimeSheetManagement.Models
{
    [Table("QA_Audit_Appeal")]
    public class AppealSheet
    {
        [Key]        
        public int Audit_MasterID { get; set; }
        public int Audit_AppealID { get; set; }
        public DateTime Appealed_On { get; set; }
        public string User_Comments { get; set; }      
        public string Auditor_Comments { get; set; }
        public string Acceptance_Status { get; set; }
        public string Appeal_Status { get; set; }     
       
    }
}
