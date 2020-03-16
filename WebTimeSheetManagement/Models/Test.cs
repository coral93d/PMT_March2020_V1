using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebTimeSheetManagement.Models
{
    public class Audit_Master
    {
        [Key]
        public int Audit_MasterID { get; set; }
        public string Team_Name { get; set; }
        public DateTime? Date_Created { get; set; }
        public int Auditor_ID { get; set; }
        public string UserName { get; set; }
        public string Agent_Supervisor { get; set; }
        public string Agent_Manager { get; set; }
        public string CPU { get; set; }
        public string Patient_ID { get; set; }
        public string Invoice_Number { get; set; }
        public DateTime? Audit_Date { get; set; }
        public DateTime? Date_Of_Transaction { get; set; }
        public string Call_Details { get; set; }
        public DateTime? Date_Of_Service { get; set; }
        public string Complaint_Comments { get; set; }
        public string Call_Duration { get; set; }
        public string Details_Call { get; set; }
        public string Current_Lac { get; set; }
        public string Previous_Lac { get; set; }
        public string Call_Disposition { get; set; }
        public string Timeof_Call { get; set; }
        public string Call_ID { get; set; }
        public string Overall_Status { get; set; }

        public string Final_Status { get; set; }
        public string Attachments { get; set; }
        public string Auditor_Comments { get; set; }

        public bool Complaint { get; set; }


       
    }

  
    public class ItemStockModels
    {
        public int Forms_ID { get; set; }
        public string Questions { get; set; }
        public string Form_Name { get; set; }
        public string Metric { get; set; }
        public bool Is_Selected { get; set; }
        public int Max_Score { get; set; }
    }

    public class ItemStockContext : DbContext
    {
        public DbSet<ItemStockModels> StockModel { get; set; }
    }
}
