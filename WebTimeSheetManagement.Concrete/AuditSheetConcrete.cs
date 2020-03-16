using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;
using System.Linq.Dynamic;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace WebTimeSheetManagement.Concrete
{
    public class AuditSheetConcrete : IAuditSheet
    {
        public int AddAuditSheetMaster(AuditSheetMaster AuditSheetMaster)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.AuditSheetMaster.Add(AuditSheetMaster);
                    _context.SaveChanges();
                    int id = AuditSheetMaster.Audit_MasterID; // Yes it's here
                    return id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateAuditSheetMaster(AuditSheetMaster AuditSheetMaster)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var entity = _context.AuditSheetMaster.Find(AuditSheetMaster.Audit_MasterID);
                    _context.Entry(entity).CurrentValues.SetValues(AuditSheetMaster);
                    _context.SaveChanges();
                    int id = AuditSheetMaster.Audit_MasterID;
                    return id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int EditAuditSheetDetails(AuditSheetDetails AuditSheetDetails)
        {
            try
            {

                using (var _context = new DatabaseContext())
                {
                    var entity = _context.AuditSheetDetails.Find(AuditSheetDetails.Audit_ID);
                    _context.Entry(entity).CurrentValues.SetValues(AuditSheetDetails);


                    _context.SaveChanges();
                    int id = AuditSheetDetails.Audit_MasterID;
                    return id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Formfields> ListofFormsField(int FormsCode)
        {
            {

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
                {
                    con.Open();
                    try
                    {
                        var param = new DynamicParameters();
                        param.Add("@FormsCode", FormsCode);
                        var result = con.Query<Formfields>("Usp_GetFormsfield", param, null, true, 0, System.Data.CommandType.StoredProcedure).ToList();
                        return result;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }
        public List<TrackMaster> GetListofTrack()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var listofTrack = (from track in _context.TrackMaster
                                       select track).ToList();
                    return listofTrack;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<AuditForm> GetListofForms()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    IEnumerable<AuditForm> listofForms = new List<AuditForm>();

                    var listofFormss = _context.AuditedForms.GroupBy(l => l.FormName)
                      .Select(g => g.FirstOrDefault())
                      .ToList();

                    listofForms = listofFormss.Select(x =>
                                new AuditForm()
                                {
                                    FormsID = x.FormsID,
                                    FormName = x.FormName,
                                    FormsCode = x.FormsCode,
                                  
                                });


                    return listofForms.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int DeleteAuditsheetByOnlyAudit_MasterID(int Audit_MasterID)
        {
            int resultAuditSheetMaster = 0;
            int resultAuditSheetDetails = 0;
            try
            {
                using (var _context = new DatabaseContext())
                {

                    var auditsheetcount = (from ex in _context.AuditSheetMaster
                                          where ex.Audit_MasterID == Audit_MasterID
                                           select ex).Count();

                    if (auditsheetcount > 0)
                    {
                        AuditSheetMaster auditsheet = (from ex in _context.AuditSheetMaster
                                                       where ex.Audit_MasterID == Audit_MasterID
                                                       select ex).SingleOrDefault();

                        _context.AuditSheetMaster.Remove(auditsheet);
                        resultAuditSheetMaster = _context.SaveChanges();
                    }

                    var auditsheetdetailscount = (from ex in _context.AuditSheetDetails
                                                 where ex.Audit_MasterID == Audit_MasterID
                                                  select ex).Count();

                    if (auditsheetdetailscount > 0)
                    {

                        var auditsheetdetails = (from ex in _context.AuditSheetDetails
                                                 where ex.Audit_MasterID == Audit_MasterID
                                                 select ex).ToList();

                        _context.AuditSheetDetails.RemoveRange(auditsheetdetails);
                        resultAuditSheetDetails = _context.SaveChanges();

                    }

                    if (resultAuditSheetMaster > 0 || resultAuditSheetDetails > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int AddAuditSheetDetail(AuditSheetDetails AuditSheetDetails)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.AuditSheetDetails.Add(AuditSheetDetails);
                    _context.SaveChanges();
                    int id = AuditSheetDetails.Audit_ID; // Yes it's here
                    return id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Auditsheet count for QA
        public DisplayViewModel GetAuditSheetsCountByQualityID(string QualityID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                param.Add("@QualityID", QualityID);
                return con.Query<DisplayViewModel>("Usp_GetAuditSheetsCountByQualityID",
                    param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public DisplayViewModel GetAuditSheetsCountForQualityLead()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                //param.Add("@QualityID", QualityID);
                return con.Query<DisplayViewModel>("Usp_GetAuditSheetsCountForQualityLead",
                    param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public DisplayViewModel GetAuditSheetsCountByUserID(string UserID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                var param = new DynamicParameters();
                param.Add("@UserID", UserID);
                return con.Query<DisplayViewModel>("Usp_GetAuditSheetsCountByUserID ", param, null, true, 0, System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }
        public void InsertAuditSheetAuditLog(AuditSheetAuditTB auditsheetaudittb)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    _context.AuditSheetAuditTB.Add(auditsheetaudittb);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<AuditSheetMasterView> ShowAuditSheet_QALead(string sortColumn, string sortColumnDir, string Search)
        {
            var _context = new DatabaseContext();


            var IQueryableauditsheet = (from auditsheetmaster in _context.AuditSheetMaster
                                        join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                        join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                        join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                        
                                        select new AuditSheetMasterView
                                        {
                                            Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                            Invoice_Number = auditsheetmaster.Invoice_Number,
                                            Username = b.Name,
                                            Auditor_Name = bb.Name,
                                            Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                                            SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                                            SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                            Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                                            SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                                            SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                            Audit_Type = auditsheetmaster.Audit_Type,
                                            Final_Marks = auditsheetmaster.Final_Status,
                                            Result = auditsheetmaster.Overall_Status,

                                            AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                        });


            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableauditsheet = IQueryableauditsheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableauditsheet = IQueryableauditsheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));
            }

            return IQueryableauditsheet;

        }
        public IQueryable<AuditSheetMasterView> ShowAuditSheet(string sortColumn, string sortColumnDir, string Search, int Quality)
        {
            var _context = new DatabaseContext();


            var IQueryableauditsheet = (from auditsheetmaster in _context.AuditSheetMaster
                                        join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                        join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                        join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                        where bb.RegistrationID == Quality
                                        select new AuditSheetMasterView
                                        {
                                             Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                            Invoice_Number = auditsheetmaster.Invoice_Number,
                                            Username = b.Name,
                                            Auditor_Name = bb.Name,
                                            Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                                            SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                                            SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                            Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                                            SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                                            SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                            Audit_Type = auditsheetmaster.Audit_Type,
                                            Final_Marks = auditsheetmaster.Final_Status,
                                            Result = auditsheetmaster.Overall_Status,

                                            AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                        });


            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableauditsheet = IQueryableauditsheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableauditsheet = IQueryableauditsheet.Where(m => m.Invoice_Number.Contains(Search)|| m.Username.Contains(Search)||m.Transaction_Date.Contains(Search)||m.AuditSheetStatus.Contains(Search)||m.Audit_Date.Contains(Search));
            }

            return IQueryableauditsheet;

        }
        //Show Audit to user
        public IQueryable<AuditSheetMasterView> ShowAuditSheetuser(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();


            var IQueryableauditsheet = (from auditsheetmaster in _context.AuditSheetMaster
                                        join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                        join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                        join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                        where b.RegistrationID == UserID
                                        select new AuditSheetMasterView
                                        {
                                            Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                            Invoice_Number = auditsheetmaster.Invoice_Number,
                                            Username = b.Name,
                                            Auditor_Name = bb.Name,
                                            Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                                            SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                                            SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                            Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                                            SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                                            SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                            Audit_Type = auditsheetmaster.Audit_Type,
                                            Final_Marks = auditsheetmaster.Final_Status,
                                            Result = auditsheetmaster.Overall_Status,

                                            AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                        });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableauditsheet = IQueryableauditsheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableauditsheet = IQueryableauditsheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryableauditsheet;

        }
        public List<AuditSheetDetailsView> AuditsheetDetailsbyAuditSheetMasterID(int UserID, int Audit_MasterID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from auditsheet in _context.AuditSheetMaster
                                // join project in _context.ProjectMaster on timesheet.ProjectID equals project.ProjectID
                            where auditsheet.UserName == UserID.ToString() && auditsheet.Audit_MasterID == Audit_MasterID
                            select new AuditSheetDetailsView
                            {
                                Audit_MasterID = auditsheet.Audit_MasterID,

                                Transaction_Date = SqlFunctions.DateName("day", auditsheet.Transaction_Date).Trim() + "/" +
                                SqlFunctions.StringConvert((double)auditsheet.Transaction_Date.Value.Month).TrimStart() + "/" +
                                SqlFunctions.DateName("year", auditsheet.Transaction_Date),

                                Date_Of_Service = SqlFunctions.DateName("day", auditsheet.Date_Of_Service).Trim() + "/" +
                                SqlFunctions.StringConvert((double)auditsheet.Date_Of_Service.Value.Month).TrimStart() + "/" +
                                SqlFunctions.DateName("year", auditsheet.Date_Of_Service),

                                Audit_Type = auditsheet.Audit_Type,
                                Marks_scored = auditsheet.Overall_Status,
                                //Timeof_Call= auditsheet.Timeof_Call,
                                Final_Status = auditsheet.Final_Status

                            }).ToList();

                return data;
            }
        }

        public List<AuditSheetDetailsView> AuditsheetDetailsbyAuditSheetMasterID(int Audit_MasterID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from auditsheet in _context.AuditSheetMaster
                            join b in _context.Registration on auditsheet.UserName equals b.Username
                            join bb in _context.Registration on auditsheet.Auditor_ID equals bb.RegistrationID
                            //join project in _context.ProjectMaster on timesheet.ProjectID equals project.ProjectID
                            where auditsheet.Audit_MasterID == Audit_MasterID
                            select new AuditSheetDetailsView
                            {
                                Audit_MasterID = auditsheet.Audit_MasterID,
                                Team_Name = auditsheet.Team_Name,
                                Case_Number = auditsheet.Invoice_Number,
                                Auditor_Name = bb.Name,
                                Mode_of_Monitoring = auditsheet.Mode_of_Monitoring,
                                UserName = b.Name,
                                Agent_Supervisor = auditsheet.Agent_Supervisor,
                                Agent_Manager = auditsheet.Agent_Manager,

                                Audit_Date = SqlFunctions.DateName("day", auditsheet.Audit_Date).Trim() + "/" +
                                SqlFunctions.StringConvert((double)auditsheet.Audit_Date.Month).TrimStart() + "/" +
                                SqlFunctions.DateName("year", auditsheet.Audit_Date),

                                Order_Date = SqlFunctions.DateName("day", auditsheet.Order_Date).Trim() + "/" +
                                SqlFunctions.StringConvert((double)auditsheet.Order_Date.Value.Month).TrimStart() + "/" +
                                SqlFunctions.DateName("year", auditsheet.Order_Date),

                                Date_Of_Service = SqlFunctions.DateName("day", auditsheet.Date_Of_Service).Trim() + "/" +
                                SqlFunctions.StringConvert((double)auditsheet.Date_Of_Service.Value.Month).TrimStart() + "/" +
                                SqlFunctions.DateName("year", auditsheet.Date_Of_Service),
                                CPU = auditsheet.CPU,
                                ACIS_ID = auditsheet.ACIS_ID,
                                Denial_Date = SqlFunctions.DateName("day", auditsheet.Denial_Date).Trim() + "/" +
                                SqlFunctions.StringConvert((double)auditsheet.Denial_Date.Value.Month).TrimStart() + "/" +
                                SqlFunctions.DateName("year", auditsheet.Denial_Date),

                                Transaction_Date = SqlFunctions.DateName("day", auditsheet.Transaction_Date).Trim() + "/" +
                                SqlFunctions.StringConvert((double)auditsheet.Transaction_Date.Value.Month).TrimStart() + "/" +
                                SqlFunctions.DateName("year", auditsheet.Transaction_Date),

                                Denial_Reason = auditsheet.Denial_Reason,
                                Invoice_Number = auditsheet.Invoice_Number,
                                Payor_ID = auditsheet.Payor_ID,
                                Equipment = auditsheet.Equipment,
                                Audit_Type = auditsheet.Audit_Type,
                                Current_Lac = auditsheet.Current_Lac,
                                Previous_Lac = auditsheet.Previous_Lac,
                                Call_ID = auditsheet.Call_ID,
                                Invoice_Balance = auditsheet.Invoice_Balance,
                                Call_Disposition = auditsheet.Call_Disposition,
                                Is_AR_Issuelog_Open = auditsheet.Is_AR_Issuelog_Open,
                                Call_Duration = auditsheet.Call_Duration,
                                Need_Training = auditsheet.Need_Training,
                                Coaching_Comments = auditsheet.Coaching_Comments,
                                Timeof_Call = auditsheet.Timeof_Call,
                                BuzzRule_Loaded_or_not =auditsheet.BuzzRule_Loaded_or_not,
                                Alternate_LAC =auditsheet.Alternate_LAC,
                                Correct_Outcome =auditsheet.Correct_Outcome,
                                Overall_Status = auditsheet.Overall_Status,
                                AuditSheetStatus =auditsheet.AuditSheetStatus

                            }).ToList();

                return data;
            }
        }

        //Edit Audit Master for QA
        public List<AuditMaster> EditAuditMasterID(int Audit_MasterID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from auditsheet in _context.AuditSheetMaster
                            join b in _context.Registration on auditsheet.UserName equals b.Username
                            join bb in _context.Registration on auditsheet.Auditor_ID equals bb.RegistrationID
                            //join project in _context.ProjectMaster on timesheet.ProjectID equals project.ProjectID
                            where auditsheet.Audit_MasterID == Audit_MasterID

                            select new AuditMaster
                            {
                                Audit_MasterID = auditsheet.Audit_MasterID,
                                Audit_Date = auditsheet.Audit_Date,
                                Mode_of_Monitoring = auditsheet.Mode_of_Monitoring,
                                Team_Name = auditsheet.Team_Name,                               
                                Auditor_ID = auditsheet.Auditor_ID,
                                UserName = auditsheet.UserName,
                                Agent_Supervisor = auditsheet.Agent_Supervisor,
                                Agent_Manager = auditsheet.Agent_Manager,
                                Transaction_Date = auditsheet.Transaction_Date,
                                Coaching_Comments = auditsheet.Coaching_Comments,
                                Invoice_Balance = auditsheet.Invoice_Balance,
                                Need_Training = auditsheet.Need_Training,
                                Order_Date = auditsheet.Order_Date,
                                CPU = auditsheet.CPU,
                                Invoice_Number = auditsheet.Invoice_Number,
                                Denial_Date = auditsheet.Denial_Date,
                                Equipment = auditsheet.Equipment,
                                Current_Lac = auditsheet.Current_Lac,
                                Call_Disposition = auditsheet.Call_Disposition,
                                Call_ID = auditsheet.Call_ID,
                                Call_Duration = auditsheet.Call_Duration,
                                Is_AR_Issuelog_Open = auditsheet.Is_AR_Issuelog_Open,
                                Date_Of_Service = auditsheet.Date_Of_Service,                             
                                ACIS_ID = auditsheet.ACIS_ID,
                                Denial_Reason = auditsheet.Denial_Reason,                                
                                Payor_ID = auditsheet.Payor_ID,                              
                                Audit_Type = auditsheet.Audit_Type,                              
                                Previous_Lac = auditsheet.Previous_Lac,
                                Timeof_Call = auditsheet.Timeof_Call,
                                BuzzRule_Loaded_or_not = auditsheet.BuzzRule_Loaded_or_not,
                                Alternate_LAC = auditsheet.Alternate_LAC,
                                Correct_Outcome = auditsheet.Correct_Outcome,
                                Overall_Status = auditsheet.Overall_Status,
                                Final_Status= auditsheet.Final_Status,
                                AuditSheetStatus= auditsheet.AuditSheetStatus
                            }).ToList();

                return data;
            }
        }
       public List<AuditMasterDetails> EditAuditDetailsMasterID(int Audit_MasterID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from auditsheet in _context.AuditSheetDetails
                            join b in _context.AuditedForms on auditsheet.FormsCode equals b.FormsCode
                            where auditsheet.Audit_MasterID == Audit_MasterID
                            select new AuditMasterDetails
                            {
                                
                                Audit_ID = auditsheet.Audit_ID,
                                Audit_MasterID = auditsheet.Audit_MasterID, 
                                FormsCode = auditsheet.FormsCode,                               
                                FieldID = auditsheet.FieldID,
                                Questions = auditsheet.Questions,
                                Parameter_Status = auditsheet.Parameter_Status,
                                Metric = auditsheet.Metric,
                                Max_Score = auditsheet.Max_Score                                
                            }).Distinct().ToList();


                return data;
            }

        }
        public List<AuditSheetDetailsViewforms> AuditsheetDetailsviewbyAuditSheetMasterID(int Audit_MasterID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from auditsheet in _context.AuditSheetDetails
                            join b in _context.AuditedForms on auditsheet.FormsCode equals b.FormsCode
                            where auditsheet.Audit_MasterID == Audit_MasterID
                            select new AuditSheetDetailsViewforms
                            {
                                Audit_MasterID = auditsheet.Audit_MasterID,
                                Audit_ID = auditsheet.Audit_ID,
                                FieldID = auditsheet.FieldID,
                                FormsCode = b.FormsCode,
                                Form_Name = b.FormName,
                                Questions = auditsheet.Questions,
                                Metric = auditsheet.Metric,
                                Max_Score = auditsheet.Max_Score,
                                Parameter_Status = auditsheet.Parameter_Status,
                                Final_Score = auditsheet.Final_Score
                            }).Distinct().ToList();


                return data;
            }
        }
        public bool UpdateAuditSheetStatus(AuditSheetApproval auditsheetapprovalmodel, int Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Audit_MasterID", auditsheetapprovalmodel.Audit_MasterID);
                    param.Add("@User_Comments", auditsheetapprovalmodel.User_Comments);
                    param.Add("@Acceptance_Status", Status);
                    var result = con.Execute("Usp_UpdateAuditSheetStatus", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }
        public bool IsAuditsheetALreadyProcessed(int Audit_MasterID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from auditsheet in _context.AuditSheetAuditTB
                            where auditsheet.Audit_MasterID == Audit_MasterID && auditsheet.Acceptance_Status != 1
                            select auditsheet).Count();

                if (data > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool UpdateAuditSheetAuditStatus(int Audit_MasterID, string User_Comments, string Acceptance_Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Audit_MasterID", Audit_MasterID);
                    param.Add("@User_Comments", User_Comments);
                    param.Add("@Acceptance_Status", Acceptance_Status);
                    var result = con.Execute("Usp_ChangeAuditsheetStatus", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }

        public IQueryable<AuditSheetMasterView> ShowAllSubmittedAuditSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();


            var IQueryableauditsheet = (from auditsheetmaster in _context.AuditSheetMaster
                                        join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                        join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                        join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                        where b.RegistrationID == UserID && d.Acceptance_Status==1
                                        select new AuditSheetMasterView
                                        {
                                            Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                            Invoice_Number = auditsheetmaster.Invoice_Number,
                                            Username = b.Name,
                                            Auditor_Name = bb.Name,
                                            Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                            Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                            Audit_Type = auditsheetmaster.Audit_Type,
                                            Final_Marks = auditsheetmaster.Final_Status,
                                            Result = auditsheetmaster.Overall_Status,

                                            AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                        });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableauditsheet = IQueryableauditsheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableauditsheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryableauditsheet;

        }
        public IQueryable<AuditSheetMasterView> ShowAllApprovedAuditSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryableauditsheet = (from auditsheetmaster in _context.AuditSheetMaster
                                        join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                        join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                        join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                        where b.RegistrationID == UserID && d.Acceptance_Status == 2
                                        select new AuditSheetMasterView
                                        {
                                            Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                            Invoice_Number = auditsheetmaster.Invoice_Number,
                                            Username = b.Name,
                                            Auditor_Name = bb.Name,
                                            Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                            Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                            Audit_Type = auditsheetmaster.Audit_Type,
                                            Final_Marks = auditsheetmaster.Final_Status,
                                            Result = auditsheetmaster.Overall_Status,

                                            AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                        });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableauditsheet = IQueryableauditsheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableauditsheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryableauditsheet;

        }

        //Auditot
        public IQueryable<AuditSheetMasterView> ShowAllAppeal_Lead(string sortColumn, string sortColumnDir, string Search)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from auditsheetmaster in _context.AuditSheetMaster
                                       join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                       join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                       join d in _context.AppealSheetDetails on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                       where  d.Audit_MasterID == auditsheetmaster.Audit_MasterID

                                       select new AuditSheetMasterView
                                       {
                                           Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                           Invoice_Number = auditsheetmaster.Invoice_Number,
                                           Username = b.Name,
                                           Auditor_Name = bb.Name,
                                           Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                           Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                           Audit_Type = auditsheetmaster.Audit_Type,
                                           Final_Marks = auditsheetmaster.Final_Status,
                                           Result = auditsheetmaster.Overall_Status,
                                           Appeal_Status = d.Appeal_Status == 1.ToString() ? "Inprogress" : d.Appeal_Status == 2.ToString() ? "Approved" : "Rejected",
                                           AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"

                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryabletimesheet;

        }



        public IQueryable<AuditSheetMasterView> ShowAllAppeal(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from auditsheetmaster in _context.AuditSheetMaster
                                       join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                       join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                       join d in _context.AppealSheetDetails on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                       where bb.RegistrationID == UserID && d.Audit_MasterID == auditsheetmaster.Audit_MasterID

                                       select new AuditSheetMasterView
                                       {
                                           Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                           Invoice_Number = auditsheetmaster.Invoice_Number,
                                           Username = b.Name,
                                           Auditor_Name = bb.Name,
                                           Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                           Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                           Audit_Type = auditsheetmaster.Audit_Type,
                                           Final_Marks = auditsheetmaster.Final_Status,
                                           Result = auditsheetmaster.Overall_Status,
                                           Appeal_Status = d.Appeal_Status == 1.ToString() ? "Inprogress" : d.Appeal_Status == 2.ToString() ? "Approved" : "Rejected",
                                           AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"

                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryabletimesheet;

        }

        //User All Appeal

        public IQueryable<AuditSheetMasterView> ShowAllAppealuser(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from auditsheetmaster in _context.AuditSheetMaster
                                       join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                       join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                       join d in _context.AppealSheetDetails on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                       where b.RegistrationID == UserID && d.Audit_MasterID == auditsheetmaster.Audit_MasterID

                                       select new AuditSheetMasterView
                                       {
                                           Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                           Invoice_Number = auditsheetmaster.Invoice_Number,
                                           Username = b.Name,
                                           Auditor_Name = bb.Name,
                                           Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                           Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                           Audit_Type = auditsheetmaster.Audit_Type,
                                           Final_Marks = auditsheetmaster.Final_Status,
                                           Result = auditsheetmaster.Overall_Status,
                                           Appeal_Status = d.Appeal_Status == 1.ToString() ? "Inprogress" : d.Appeal_Status == 2.ToString() ? "Approved" : "Rejected",
                                           AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"

                                       });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryabletimesheet;

        }
        public IQueryable<AuditSheetMasterView> ShowAllAppealedAuditSheet(string sortColumn, string sortColumnDir, string Search, int UserID)
        {
            var _context = new DatabaseContext();

            var IQueryableauditsheet = (from auditsheetmaster in _context.AuditSheetMaster
                                        join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                        join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                        join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                        where b.RegistrationID == UserID && d.Acceptance_Status == 3
                                        select new AuditSheetMasterView
                                        {
                                            Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                            Invoice_Number = auditsheetmaster.Invoice_Number,
                                            Username = b.Name,
                                            Auditor_Name = b.Name,
                                            Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                            Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                            Audit_Type = auditsheetmaster.Audit_Type,
                                            Final_Marks = auditsheetmaster.Final_Status,
                                            Result = auditsheetmaster.Overall_Status,

                                            AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                        });

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryableauditsheet = IQueryableauditsheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryableauditsheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryableauditsheet;

        }

        public IQueryable<AuditSheetMasterView> ShowSubmittedAuditSheet_Lead(string sortColumn, string sortColumnDir, string Search,  int Quality)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from auditsheetmaster in _context.AuditSheetMaster
                                       join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                       join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                       join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                       where  d.Acceptance_Status == 1
                                       select new AuditSheetMasterView
                                       {
                                           Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                           Invoice_Number = auditsheetmaster.Invoice_Number,
                                           Username = b.Name,
                                           Auditor_Name = bb.Name,
                                           Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                   SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                           Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                   SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                   SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                           Audit_Type = auditsheetmaster.Audit_Type,
                                           Final_Marks = auditsheetmaster.Final_Status,
                                           Result = auditsheetmaster.Overall_Status,

                                           AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                       });




            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryabletimesheet;

        }

        public IQueryable<AuditSheetMasterView> ShowAuditSheetStatus(string sortColumn, string sortColumnDir, string Search, int UserID, int Quality)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from auditsheetmaster in _context.AuditSheetMaster
                                        join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                        join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                        join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                        where bb.RegistrationID == UserID && d.Acceptance_Status == 1
                                        select new AuditSheetMasterView
                                        {
                                            Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                            Invoice_Number = auditsheetmaster.Invoice_Number,
                                            Username = b.Name,
                                            Auditor_Name = bb.Name,
                                            Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                            Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                    SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                    SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                            Audit_Type = auditsheetmaster.Audit_Type,
                                            Final_Marks = auditsheetmaster.Final_Status,
                                            Result = auditsheetmaster.Overall_Status,

                                            AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                        });


   

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryabletimesheet;

        }
        public IQueryable<AuditSheetMasterView> ShowApprovedAuditSheet(string sortColumn, string sortColumnDir, string Search, int UserID, int Quality)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from auditsheetmaster in _context.AuditSheetMaster
                                        join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                        join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                        join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                        where bb.RegistrationID == UserID && d.Acceptance_Status == 2
                                        select new AuditSheetMasterView
                                        {
                                            Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                            Invoice_Number = auditsheetmaster.Invoice_Number,
                                            Username = b.Name,
                                            Auditor_Name = bb.Name,
                                            Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                        SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                        SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                            Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                        SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                        SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                            Audit_Type = auditsheetmaster.Audit_Type,
                                            Final_Marks = auditsheetmaster.Final_Status,
                                            Result = auditsheetmaster.Overall_Status,

                                            AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                        });




            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryabletimesheet;

        }

        public IQueryable<AuditSheetMasterView> ShowApprovedAuditSheet_Lead(string sortColumn, string sortColumnDir, string Search, int Quality)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from auditsheetmaster in _context.AuditSheetMaster
                                       join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                       join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                       join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                       where d.Acceptance_Status == 2
                                       select new AuditSheetMasterView
                                       {
                                           Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                           Invoice_Number = auditsheetmaster.Invoice_Number,
                                           Username = b.Name,
                                           Auditor_Name = bb.Name,
                                           Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                       SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                       SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                           Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                       SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                       SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                           Audit_Type = auditsheetmaster.Audit_Type,
                                           Final_Marks = auditsheetmaster.Final_Status,
                                           Result = auditsheetmaster.Overall_Status,

                                           AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                       });




            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryabletimesheet;

        }
        public IQueryable<AuditSheetMasterView> ShowAppealedAuditSheet(string sortColumn, string sortColumnDir, string Search, int UserID, int Quality)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from auditsheetmaster in _context.AuditSheetMaster
                                        join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                        join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                        join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                        where bb.RegistrationID == UserID && d.Acceptance_Status == 3
                                        select new AuditSheetMasterView
                                        {
                                            Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                            Invoice_Number = auditsheetmaster.Invoice_Number,
                                            Username = b.Name,
                                            Auditor_Name = bb.Name,
                                            Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                        SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                        SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                            Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                        SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                        SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                            Audit_Type = auditsheetmaster.Audit_Type,
                                            Final_Marks = auditsheetmaster.Final_Status,
                                            Result = auditsheetmaster.Overall_Status,

                                            AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                        });




            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryabletimesheet;

        }

        public IQueryable<AuditSheetMasterView> ShowAppealedAuditSheet_Lead(string sortColumn, string sortColumnDir, string Search, int Quality)
        {
            var _context = new DatabaseContext();

            var IQueryabletimesheet = (from auditsheetmaster in _context.AuditSheetMaster
                                       join b in _context.Registration on auditsheetmaster.UserName equals b.Username
                                       join bb in _context.Registration on auditsheetmaster.Auditor_ID equals bb.RegistrationID
                                       join d in _context.AuditSheetAuditTB on auditsheetmaster.Audit_MasterID equals d.Audit_MasterID
                                       where d.Acceptance_Status == 3
                                       select new AuditSheetMasterView
                                       {
                                           Audit_MasterID = auditsheetmaster.Audit_MasterID,
                                           Invoice_Number = auditsheetmaster.Invoice_Number,
                                           Username = b.Name,
                                           Auditor_Name = bb.Name,
                                           Audit_Date = SqlFunctions.DateName("day", auditsheetmaster.Audit_Date).Trim() + "/" +
                       SqlFunctions.StringConvert((double)auditsheetmaster.Audit_Date.Month).TrimStart() + "/" +
                       SqlFunctions.DateName("year", auditsheetmaster.Audit_Date),

                                           Transaction_Date = SqlFunctions.DateName("day", auditsheetmaster.Transaction_Date).Trim() + "/" +
                       SqlFunctions.StringConvert((double)auditsheetmaster.Transaction_Date.Value.Month).TrimStart() + "/" +
                       SqlFunctions.DateName("year", auditsheetmaster.Transaction_Date),

                                           Audit_Type = auditsheetmaster.Audit_Type,
                                           Final_Marks = auditsheetmaster.Final_Status,
                                           Result = auditsheetmaster.Overall_Status,

                                           AuditSheetStatus = auditsheetmaster.AuditSheetStatus == 1 ? "Submitted" : auditsheetmaster.AuditSheetStatus == 2 ? "Accepted" : "Appeal"
                                       });




            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                IQueryabletimesheet = IQueryabletimesheet.OrderBy(sortColumn + " " + sortColumnDir);
            }
            if (!string.IsNullOrEmpty(Search))
            {
                IQueryabletimesheet.Where(m => m.Invoice_Number.Contains(Search) || m.Username.Contains(Search) || m.Transaction_Date.Contains(Search) || m.AuditSheetStatus.Contains(Search) || m.Audit_Date.Contains(Search));

            }

            return IQueryabletimesheet;

        }
        public int EditAuditSheetMaster(AuditSheetDetails AuditSheetDetails)
        {
            try
            {

                using (var _context = new DatabaseContext())
                {
                    var entity = _context.AuditSheetDetails.Find(AuditSheetDetails.Audit_ID);
                    _context.Entry(entity).CurrentValues.SetValues(AuditSheetDetails);


                    _context.SaveChanges();
                    int id = AuditSheetDetails.Audit_MasterID; 
                    return id;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
     
        public bool UpdateAuditSheetStatusAppeal(AuditSheetApproval auditsheetapprovalmodel, int Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Audit_MasterID", auditsheetapprovalmodel.Audit_MasterID);
                    param.Add("@User_Comments", auditsheetapprovalmodel.User_Comments);
                    param.Add("@Acceptance_Status", Status);
                    var result = con.Execute("Usp_UpdateAuditSheetStatus", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }
        //div

        public List<AppealSheetDetails> CommentsbyAuditSheetMasterID(int Audit_MasterID)
        {
            using (var _context = new DatabaseContext())
            {
                var data = (from auditsheet in _context.AuditSheetMaster
                            join b in _context.AppealSheetDetails on auditsheet.Audit_MasterID equals b.Audit_MasterID
                            where auditsheet.Audit_MasterID == Audit_MasterID
                            select new AppealSheetDetails
                            {
                                Audit_MasterID = auditsheet.Audit_MasterID,
                                User_Comments = b.User_Comments,
                                Auditor_Comments = b.Auditor_Comments
                            }).Distinct().ToList();


                return data;
            }
        }
        public bool UpdateAppeal(AuditSheetAppeal auditSheetAppealmodel, int Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Audit_MasterID", auditSheetAppealmodel.Audit_MasterID);
                    param.Add("@Auditor_Comments", auditSheetAppealmodel.Auditor_Comments);
                    param.Add("@Acceptance_Status", Status);
                    var result = con.Execute("Usp_UpdateAppeal", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }

        public bool UpdateAuditSheetStatusAppeal(int Audit_MasterID, int AuditSheetStatus, string Final_Status, string Overall_Status, string Auditor_Comments, string Appeal_Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Audit_MasterID", Audit_MasterID);
                    param.Add("@Overall_Status", Overall_Status);
                    param.Add("@Final_Status", Final_Status);
                    param.Add("@AuditSheetStatus", AuditSheetStatus);
                    param.Add("@Auditor_Comments", Auditor_Comments);
                    param.Add("@Appeal_Status", Appeal_Status);



                    var result = con.Execute("Usp_UpdateAuditmasterappeal", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }

        public bool UpdateAuditSheetAppeal(AuditSheetApproval auditsheetapprovalmodel, string Acceptance_Status, string Appeal_Status)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TimesheetDBEntities"].ToString()))
            {
                con.Open();
                SqlTransaction sql = con.BeginTransaction();

                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Audit_MasterID", auditsheetapprovalmodel.Audit_MasterID);
                    param.Add("@User_Comments", auditsheetapprovalmodel.User_Comments);
                    param.Add("@Acceptance_Status", Acceptance_Status);
                    param.Add("@Appeal_Status", Appeal_Status);
                    var result = con.Execute("Usp_UpdateAuditAppeal", param, sql, 0, System.Data.CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        sql.Commit();
                        return true;
                    }
                    else
                    {
                        sql.Rollback();
                        return false;
                    }
                }
                catch (Exception)
                {
                    sql.Rollback();
                    throw;
                }
            }
        }
    }
}
