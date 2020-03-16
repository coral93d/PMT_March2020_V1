using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTimeSheetManagement.Models
{
    [NotMapped]
    public class AuditSheetModel
    {
        //   public DateTime hdtext1 { get; set; }
        public List<Formfields> Formfields { get; set; }
        public List<TrackMaster> GetListofTrack { get; set; }

       public  List<AuditForm> GetListofForms { get; set; }
        public int FormsCode { get; set; }
        public int Fieldcount { get; set; }
        public string Call_p1 { get; set; }

        public string TeamName_p1 { get; set; } //Team Name

        public int TAuditor_ID_p1 { get; set; } //Auditor_ID
        public string UserName { get; set; } //UserName    
                                             // public DateTime hdtext1 { get; set; } //Date Created

        public string Agent_Supervisor { get; set; } //Agent_Supervisor 
        public string Agent_Manager { get; set; } //Agent_Manager
        public string textt1_p1 { get; set; } //Coaching_Comments
        public string textt2_p1 { get; set; } //BUZZ RULE LOADED OR NOT
        public string textt3_p1 { get; set; } //Need_Training
        public DateTime? hdtext2 { get; set; } //Order_Date
        public string text4_p1 { get; set; } //CPU
        public DateTime? hdtext3 { get; set; } //Denial_Date   
        public string textt6_p1 { get; set; } //Invoice_Number
        public string textt7_p1 { get; set; } //Equipment
        public string text8_p1 { get; set; } //Current_Lac
        public string text9_p1 { get; set; } //Call_Disposition
        public string text10_p1 { get; set; } //Call_ID
        public string text11_p1 { get; set; } //Call_Duration
        public string text12_p1 { get; set; } //AR ISSUE log open
        public DateTime? hdtext4 { get; set; } //Date_Of_Service
        public DateTime? hdtext5 { get; set; } //Transaction date

        public string text13_p1 { get; set; } //ACIS ID
        public string text14_p1 { get; set; } //Denial_Reason
        public string text15_p1 { get; set; } //Payor_ID
        public string text16_p1 { get; set; } //Audit_Type

        public string text17_p1 { get; set; } //Previous_Lac
        public string text18_p1 { get; set; } //Timeof_Call
        public string text19_p1 { get; set; } //Overall_Status

        public string text21_p1 { get; set; } //Invoice Balance
        public string text22_p1 { get; set; } //Alternate LAC
        public string text23_p1 { get; set; } //Correct outcome
        public string text20_p1 { get; set; } //Final_Status
        public string Mode_of_Monitoring { get; set; } //Mode_of_Monitoring


        [RegularExpression(@"^\d+$", ErrorMessage = "Enter Only Numbers")]
        public int? texttotal_p1 { get; set; }  //toatl score_overall status calculation based on Metric

        [StringLength(50, ErrorMessage = "Length Exceeds")]
        public string Description_p1 { get; set; }


        [Required(ErrorMessage = "Choose Project")]
        public int? TrackID1 { get; set; }
        public int FormID1 { get; set; }


    }

    public class Formfields
    {
        public int FieldID { get; set; }
        public string Questions { get; set; }
        public bool Parameter_Status { get; set; }
        public string Metric { get; set; }
        public int Max_Score { get; set; }
        public int Final_Score { get; set; }

        public string FormName { get; set; }
        //   public List<Formfields> Formfields = new List<Formfields>();

    }

    public class AuditForm
    {
        public int FormsID { get; set; }

       
        public string FormName { get; set; }

        public int FormsCode { get; set; }

    }





}
