using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTimeSheetManagement.Models
{
    [Table("QA_Audit_Master")]
    public class AuditSheetMaster
    {
        [Key]
        public int Audit_MasterID { get; set; }
        public DateTime Audit_Date { get; set; }
        public string Mode_of_Monitoring { get; set; }
        //public string call_Type { get; set; }
        public string Team_Name { get; set; }
        public int Auditor_ID { get; set; }
        public string UserName { get; set; }
        public string Agent_Supervisor { get; set; }
        public string Agent_Manager { get; set; }
        public DateTime? Transaction_Date { get; set; }       
        public string Coaching_Comments { get; set; }      
        public string Need_Training { get; set; }
        public DateTime? Order_Date { get; set; }
        public string CPU { get; set; }
        public string Invoice_Number { get; set; }
        public DateTime? Denial_Date { get; set; }
        public string Equipment { get; set; }
        public string Current_Lac { get; set; }
        public string Call_Disposition { get; set; }
        public string Call_ID { get; set; }
        public string Call_Duration { get; set; }
      
        public DateTime? Date_Of_Service { get; set; }
        public string ACIS_ID { get; set; }
        public string Denial_Reason { get; set; }
        public string Payor_ID { get; set; }
        public string Audit_Type { get; set; }
        public string Previous_Lac { get; set; }
        public string Timeof_Call { get; set; }
        public string Invoice_Balance { get; set; }
        public string Is_AR_Issuelog_Open { get; set; }
        public string BuzzRule_Loaded_or_not { get; set; }
        public string Alternate_LAC { get; set; }
        public string Correct_Outcome { get; set; }
        public string Overall_Status { get; set; }
        public string Final_Status { get; set; }
        public int AuditSheetStatus { get; set; }

    }


}
