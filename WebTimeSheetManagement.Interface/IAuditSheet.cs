using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Interface
{
    public interface IAuditSheet
    {
        int AddAuditSheetMaster(AuditSheetMaster AuditSheetMaster);
        int UpdateAuditSheetMaster(AuditSheetMaster AuditSheetMaster);
        List<Formfields> ListofFormsField(int FormsCode);
        List<TrackMaster> GetListofTrack();
        List<AuditForm> GetListofForms();
        DisplayViewModel GetAuditSheetsCountByQualityID(string QualityID);
        //DisplayViewModel AuditCountFor_QALead();
        int DeleteAuditsheetByOnlyAudit_MasterID(int Audit_MasterID);
        DisplayViewModel GetAuditSheetsCountByUserID(string UserID);
        DisplayViewModel GetAuditSheetsCountForQualityLead();
        // int AddQAFormMaster(QAFormMaster QAFormMaster);
        int AddAuditSheetDetail(AuditSheetDetails AuditSheetDetails);
        IQueryable<AuditSheetMasterView> ShowAuditSheet_QALead(string sortColumn, string sortColumnDir, string Search);
        IQueryable<AuditSheetMasterView> ShowAuditSheet(string sortColumn, string sortColumnDir, string Search, int Quality);
        IQueryable<AuditSheetMasterView> ShowAuditSheetuser(string sortColumn, string sortColumnDir, string Search, int UserID);
        List<AuditSheetDetailsView> AuditsheetDetailsbyAuditSheetMasterID(int UserID, int Audit_MasterID);
        List<AppealSheetDetails> CommentsbyAuditSheetMasterID(int Audit_MasterID);
        void InsertAuditSheetAuditLog(AuditSheetAuditTB auditsheetaudittb);
        List<AuditSheetDetailsView> AuditsheetDetailsbyAuditSheetMasterID(int Audit_MasterID);
        List<AuditMaster> EditAuditMasterID(int Audit_MasterID);
        List<AuditMasterDetails> EditAuditDetailsMasterID(int Audit_MasterID);
        List<AuditSheetDetailsViewforms> AuditsheetDetailsviewbyAuditSheetMasterID(int Audit_MasterID);
        bool UpdateAuditSheetStatus(AuditSheetApproval auditsheetapprovalmodel, int Status);
        bool IsAuditsheetALreadyProcessed(int Audit_MasterID);
        bool UpdateAuditSheetAuditStatus(int Audit_MasterID, string User_Comments, string Acceptance_Status);
        IQueryable<AuditSheetMasterView> ShowAllSubmittedAuditSheet(string sortColumn, string sortColumnDir, string Search, int UserID);
        IQueryable<AuditSheetMasterView> ShowAllApprovedAuditSheet(string sortColumn, string sortColumnDir, string Search, int UserID);
        IQueryable<AuditSheetMasterView> ShowAllAppealedAuditSheet(string sortColumn, string sortColumnDir, string Search, int UserID);
        IQueryable<AuditSheetMasterView> ShowSubmittedAuditSheet_Lead(string sortColumn, string sortColumnDir, string Search,  int Quality);
        IQueryable<AuditSheetMasterView> ShowAuditSheetStatus(string sortColumn, string sortColumnDir, string Search, int UserID, int Quality);
        IQueryable<AuditSheetMasterView> ShowApprovedAuditSheet_Lead(string sortColumn, string sortColumnDir, string Search, int Quality);
        IQueryable<AuditSheetMasterView> ShowApprovedAuditSheet(string sortColumn, string sortColumnDir, string Search, int UserID, int Quality);
        IQueryable<AuditSheetMasterView> ShowAppealedAuditSheet(string sortColumn, string sortColumnDir, string Search, int UserID, int Quality);
        IQueryable<AuditSheetMasterView> ShowAppealedAuditSheet_Lead(string sortColumn, string sortColumnDir, string Search,int Quality);
        IQueryable<AuditSheetMasterView> ShowAllAppeal_Lead(string sortColumn, string sortColumnDir, string Search);
        IQueryable<AuditSheetMasterView> ShowAllAppeal(string sortColumn, string sortColumnDir, string Search, int UserID);
        IQueryable<AuditSheetMasterView> ShowAllAppealuser(string sortColumn, string sortColumnDir, string Search, int UserID);
        //bool UpdateAuditSheetStatusAppeal(int Audit_MasterID, int AuditSheetStatus, string Final_Status, string Overall_Status);
        int EditAuditSheetMaster(AuditSheetDetails AuditSheetDetails);
        int EditAuditSheetDetails(AuditSheetDetails AuditSheetDetails);
        bool UpdateAppeal(AuditSheetAppeal auditSheetAppealmodel, int Status);
        bool UpdateAuditSheetStatusAppeal(int Audit_MasterID, int AuditSheetStatus, string Final_Status, string Overall_Status, string Auditor_Comments, string Appeal_Status);
        bool UpdateAuditSheetAppeal(AuditSheetApproval auditsheetapprovalmodel, string Acceptance_Status, string Appeal_Status);
      //  void InsertAuditSheetAuditLog(AuditSheetAuditTB auditsheetaudittb);


    }
}
