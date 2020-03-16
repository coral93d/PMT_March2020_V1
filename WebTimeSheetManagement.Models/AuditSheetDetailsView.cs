using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    public class AuditSheetDetailsView
    {
        public string Call_p1 { get; set; }
        public int Audit_MasterID { get; set; }
        public string Audit_Date { get; set; }
        public string Mode_of_Monitoring { get; set; }
        public string Team_Name { get; set; }
        public int Auditor_ID { get; set; }
        public string Auditor_Name { get; set; }
        public string UserName { get; set; }
        public string Agent_Supervisor { get; set; }
        public string Agent_Manager { get; set; }
        public string Transaction_Date { get; set; }
        public string Coaching_Comments { get; set; }
      
        public string Need_Training { get; set; }
        public string Order_Date { get; set; }
        public string CPU { get; set; }
        public string Invoice_Number { get; set; }
        public string Case_Number { get; set; }
        public string Denial_Date { get; set; }
        public string Equipment { get; set; }
        public string Current_Lac { get; set; }
        public string Call_Disposition { get; set; }
        public string Call_ID { get; set; }
        public string Call_Duration { get; set; }
     
        public string Date_Of_Service { get; set; }
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
        public string Marks_scored { get; set; }

    }

    public class AuditSheetDetailsViewforms
    {
        public int FormsCode { get; set; }
        public int? FieldID { get; set; }
        public int Audit_ID { get; set; }
        public string Form_Name { get; set; }
        public int Audit_MasterID { get; set; }
        public string Questions { get; set; }
        public bool Parameter_Status { get; set; }
        public string Metric { get; set; }
        public int Max_Score { get; set; }
        public int Final_Score { get; set; }
        //  public string Final_Status { get; set; }

    }

    public class AppealSheetDetails
    {
        public int Audit_AppealID { get; set; }
        public int Audit_MasterID { get; set; }
        public DateTime? Appealed_On { get; set; }
        public string User_Comments { get; set; }
        public string Auditor_Comments { get; set; }
        public string Acceptance_Status { get; set; }
        public string Appeal_Status { get; set; }
    }

    public class MainAuditSheetView
    {
        public List<AuditSheetDetailsView> ListAuditSheetDetails { get; set; }

        public List<AuditSheetDetailsViewforms> ListAuditSheetDetailsforms { get; set; }
        public List<AppealSheetDetails> ListAppealSheetDetails { get; set; }

        // public List<AppealSheetDetails> ListAppealSheetDetails { get; set; }
        // public  List<GetPeriods> ListofPeriods { get; set; }
        //  public List<GetProjectNames> ListofProjectNames { get; set; }
        //  public List<string> ListoDayofWeek { get; set; }
        public string Appeal_Status { get; set; }
        public string Auditor_Comments { get; set; }
        public int Audit_MasterID { get; set; }
    }

    public class MainAuditSheetEdit
    {
        public List<AuditMaster> ListAuditMaster { get; set; }

        public List<AuditMasterDetails> ListAuditMasterDetails { get; set; }

        // public List<AppealSheetDetails> ListAppealSheetDetails { get; set; }
        // public  List<GetPeriods> ListofPeriods { get; set; }
        //  public List<GetProjectNames> ListofProjectNames { get; set; }
        //  public List<string> ListoDayofWeek { get; set; }
       // public string Appeal_Status { get; set; }
       // public string Auditor_Comments { get; set; }
        public int Audit_MasterID { get; set; }

       
    }

    public class AuditMaster
    {
        public string Call_p1 { get; set; }
        public int Audit_MasterID { get; set; }
        public DateTime Audit_Date { get; set; }
        public string Mode_of_Monitoring { get; set; }
        public string Team_Name { get; set; }
        public int Auditor_ID { get; set; }
        public string UserName { get; set; }
        public string Agent_Supervisor { get; set; }
        public string Agent_Manager { get; set; }
        public DateTime? Transaction_Date { get; set; }
        public string Coaching_Comments { get; set; }
       // public string Call_p1 { get; set; }
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

    public class AuditMasterDetails
    {
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
