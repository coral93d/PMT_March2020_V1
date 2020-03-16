using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateQualitySession]
    public class AuditSheetController : Controller
    {
        ITrack _ITrack;
        IAuditSheet _IAuditSheet;
        IUsers _IUsers;
        IAuditForms _IAuditForms;
        IRegistration _IRegistration;
        IFormsField _IFormsField;
        public AuditSheetController()
        {
            _ITrack = new TrackConcrete();
            _IAuditSheet = new AuditSheetConcrete();
            _IUsers = new UsersConcrete();
            _IAuditForms = new AuditFormsConcrete();
            _IRegistration = new RegistrationConcrete();
            _IFormsField = new FormsFieldConcrete();
        }
        // GET: TimeSheet
        public ActionResult Add(int FormsCode = 0)
        {
            AuditSheetModel auditSheetModel = new AuditSheetModel();
            auditSheetModel.Formfields = _IAuditSheet.ListofFormsField(FormsCode);
            auditSheetModel.GetListofTrack = _IAuditSheet.GetListofTrack();
            auditSheetModel.GetListofForms = _IAuditSheet.GetListofForms();
            return View(auditSheetModel);           
        }      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AuditSheetModel auditsheetmodel)
        {
            try
            {
                if (auditsheetmodel == null)
                {
                    ModelState.AddModelError("", "Values Posted Are Not Accurate");
                    return Json("Add", "AuditSheet");
                }

              //  var Formfields = auditsheetmodel.Formfields.ToList(); 

                AuditSheetMaster objauditsheetmaster = new AuditSheetMaster();
                objauditsheetmaster.Audit_MasterID = 0;
                objauditsheetmaster.Audit_Date = DateTime.Now;
                objauditsheetmaster.Team_Name = auditsheetmodel.TeamName_p1;
                if (Session["QualityLead"] != null)
                {
                    objauditsheetmaster.Auditor_ID = Convert.ToInt32(Session["QualityLead"]);
                }
                else
                {
                    objauditsheetmaster.Auditor_ID = Convert.ToInt32(Session["Quality"]);
                }


                 objauditsheetmaster.UserName = auditsheetmodel.UserName;
                objauditsheetmaster.Agent_Supervisor = auditsheetmodel.Agent_Supervisor;
                objauditsheetmaster.Agent_Manager = auditsheetmodel.Agent_Manager;
                objauditsheetmaster.Transaction_Date= auditsheetmodel.hdtext5;
                objauditsheetmaster.Coaching_Comments = auditsheetmodel.textt1_p1;
            //    objauditsheetmaster.call_Type = CalculateFinal_Score(auditsheetmodel).ToString(); 
                // objauditsheetmaster.First_call_Resolution = auditsheetmodel.textt2_p1;
                objauditsheetmaster.Need_Training = auditsheetmodel.textt3_p1;

                objauditsheetmaster.Order_Date = auditsheetmodel.hdtext2; //need to fix

                objauditsheetmaster.CPU = auditsheetmodel.text4_p1;
                objauditsheetmaster.Denial_Date = auditsheetmodel.hdtext3;  //need to fix

                objauditsheetmaster.Invoice_Number = auditsheetmodel.textt6_p1;
                objauditsheetmaster.Equipment = auditsheetmodel.textt7_p1;
                objauditsheetmaster.Current_Lac = auditsheetmodel.text8_p1;
                objauditsheetmaster.Call_Disposition = auditsheetmodel.text9_p1;
                objauditsheetmaster.Call_ID = auditsheetmodel.text10_p1;
                objauditsheetmaster.Call_Duration = auditsheetmodel.text11_p1;
               // objauditsheetmaster.Call_Details = auditsheetmodel.text12_p1;

                objauditsheetmaster.Date_Of_Service = auditsheetmodel.hdtext4;//need to fix
                objauditsheetmaster.ACIS_ID = auditsheetmodel.text13_p1;
                objauditsheetmaster.Denial_Reason = auditsheetmodel.text14_p1;
                objauditsheetmaster.Payor_ID = auditsheetmodel.text15_p1;
                objauditsheetmaster.Audit_Type = auditsheetmodel.text16_p1;
                objauditsheetmaster.Previous_Lac = auditsheetmodel.text17_p1;
                objauditsheetmaster.Timeof_Call = auditsheetmodel.text18_p1;
                objauditsheetmaster.Final_Status = CalculateFinal_Score(auditsheetmodel).ToString();
                objauditsheetmaster.BuzzRule_Loaded_or_not = auditsheetmodel.textt2_p1;
                objauditsheetmaster.Is_AR_Issuelog_Open = auditsheetmodel.text12_p1;
                objauditsheetmaster.Invoice_Balance = auditsheetmodel.text21_p1;
                objauditsheetmaster.Alternate_LAC = auditsheetmodel.text22_p1;
                objauditsheetmaster.Correct_Outcome = auditsheetmodel.text23_p1;
                objauditsheetmaster.Overall_Status = CalculateStatus(auditsheetmodel);
                objauditsheetmaster.Mode_of_Monitoring = auditsheetmodel.Mode_of_Monitoring;
                objauditsheetmaster.AuditSheetStatus = 1;

                int Audit_MasterID = _IAuditSheet.AddAuditSheetMaster(objauditsheetmaster);
                var UserName = auditsheetmodel.UserName;


                if (Audit_MasterID > 0)
                 {
                     Save(auditsheetmodel, Audit_MasterID);
                    _IAuditSheet.InsertAuditSheetAuditLog(InsertAuditSheetAudit(Audit_MasterID, 1, UserName));
                    
                }

                TempData["AuditCardMessage"] = "Audit Saved Successfully";

                return RedirectToAction("Add", "AuditSheet");
            }
            catch (Exception)
            {
                throw;
            }
        }      
        public ActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("AuditSheet", "Add");
                }

                MainAuditSheetEdit objMT = new MainAuditSheetEdit();

              

                objMT.ListAuditMaster = _IAuditSheet.EditAuditMasterID(Convert.ToInt32(id));
                objMT.ListAuditMasterDetails = _IAuditSheet.EditAuditDetailsMasterID(Convert.ToInt32(id));


                //objMT.ListofProjectNames = _ITimeSheet.GetProjectNamesbyTimeSheetMasterID(Convert.ToInt32(id));
                //objMT.ListofPeriods = _ITimeSheet.GetPeriodsbyTimeSheetMasterID(Convert.ToInt32(id));
                //objMT.ListoDayofWeek = DayofWeek();
                objMT.Audit_MasterID = Convert.ToInt32(id);
                return View(objMT);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult Edit(MainAuditSheetEdit objMT)
        {
            try
            {
                for (int i = 0; i < (1); i++)
                {
                    if (objMT == null)
                    {
                        ModelState.AddModelError("", "Values Posted Are Not Accurate");
                        return Json("Add", "AuditSheet");
                    }

                    var Audit_MasterID = objMT.ListAuditMaster[0].Audit_MasterID;
                    var UserName = "";
                    AuditSheetMaster objauditsheetmaster = new AuditSheetMaster();
                    //  var Formfields = auditsheetmodel.Formfields.ToList(); 
                    foreach (var item in objMT.ListAuditMaster)
                    {

                        objauditsheetmaster.Audit_MasterID = objMT.Audit_MasterID;
                        objauditsheetmaster.Audit_Date = DateTime.Now; 
                        objauditsheetmaster.Team_Name = item.Team_Name;
                        if (Session["QualityLead"] != null)
                        {
                            objauditsheetmaster.Auditor_ID = Convert.ToInt32(Session["QualityLead"]);
                        }
                        else
                        {
                            objauditsheetmaster.Auditor_ID = Convert.ToInt32(Session["Quality"]);
                        }
                        objauditsheetmaster.UserName = item.UserName;
                        objauditsheetmaster.Agent_Supervisor = item.Agent_Supervisor;
                        objauditsheetmaster.Agent_Manager = item.Agent_Manager;
                        objauditsheetmaster.Transaction_Date = item.Transaction_Date;
                        objauditsheetmaster.Coaching_Comments = item.Coaching_Comments;
                        objauditsheetmaster.Invoice_Balance = item.Invoice_Balance;
                        objauditsheetmaster.Need_Training = item.Need_Training;

                        objauditsheetmaster.Order_Date = item.Order_Date; //need to fix

                        objauditsheetmaster.CPU = item.CPU;
                        objauditsheetmaster.Denial_Date = item.Denial_Date;  //need to fix

                        objauditsheetmaster.Invoice_Number = item.Invoice_Number;
                        objauditsheetmaster.Equipment = item.Equipment;
                        objauditsheetmaster.Current_Lac = item.Current_Lac;
                        objauditsheetmaster.Call_Disposition = item.Call_Disposition;
                        objauditsheetmaster.Call_ID = item.Call_ID;
                        objauditsheetmaster.Call_Duration = item.Call_Duration;
                        objauditsheetmaster.Is_AR_Issuelog_Open= item.Is_AR_Issuelog_Open;

                        objauditsheetmaster.Date_Of_Service = item.Date_Of_Service;//need to fix
                        objauditsheetmaster.ACIS_ID = item.ACIS_ID;
                        objauditsheetmaster.Denial_Reason = item.Denial_Reason;
                        objauditsheetmaster.Payor_ID = item.Payor_ID;
                        objauditsheetmaster.Audit_Type = item.Audit_Type;
                        objauditsheetmaster.Previous_Lac = item.Previous_Lac;
                        objauditsheetmaster.Timeof_Call = item.Timeof_Call;
                        objauditsheetmaster.BuzzRule_Loaded_or_not = item.BuzzRule_Loaded_or_not;
                        objauditsheetmaster.Alternate_LAC = item.Alternate_LAC;
                        objauditsheetmaster.Correct_Outcome = item.Correct_Outcome;
                        objauditsheetmaster.Final_Status = CalculateFinal_Score(objMT).ToString();
                        objauditsheetmaster.Overall_Status = CalculateStatus(objMT);
                        objauditsheetmaster.Mode_of_Monitoring = item.Mode_of_Monitoring;
                        objauditsheetmaster.AuditSheetStatus = 1;

                      Audit_MasterID = _IAuditSheet.UpdateAuditSheetMaster(objauditsheetmaster);

                   
                    UserName = item.UserName;
                    }

                    if (Audit_MasterID > 0)
                    {
                        Save(objMT, Audit_MasterID);
                       

                    }
                }

                TempData["AuditCardMessage"] = "Audit Saved Successfully";

                return RedirectToAction("AuditSheet", "AllAuditSheet");

            }
            catch (Exception)
            {
                throw;
            }
        }
        [NonAction]
        //Calculate status Add
        private string CalculateStatus(AuditSheetModel AuditSheetModel)
        {
            try
            {
                string Overall_Status = "";
                //int? Total = 0;
                int countoffields = (AuditSheetModel.Formfields).Count();

                for (int i = 0; i < (AuditSheetModel.Formfields).Count(); i++)
                {

                    if ((AuditSheetModel.Call_p1 == "Calling"))
                    { 

                        if ((AuditSheetModel.Formfields[i].Metric == "FATAL ACCURACY") && (AuditSheetModel.Formfields[i].Parameter_Status == false))
                    {
                        Overall_Status = "Fail";
                        break;
                    }
                    else
                    {
                        if ((AuditSheetModel.Formfields[i].Metric == "FATAL ACCURACY") && (AuditSheetModel.Formfields[i].Parameter_Status == true))
                        {
                            Overall_Status = "Pass";
                        }
                    }
                    }

                    else
                    {
                        if ((AuditSheetModel.Formfields[i].FieldID != '2') && (AuditSheetModel.Formfields[i].Parameter_Status == true))
                        {
                            Overall_Status = "Pass";
                            break;
                        }
                        else {
                            if ((AuditSheetModel.Formfields[i].FieldID != '2') && (AuditSheetModel.Formfields[i].Parameter_Status == false))
                            {
                                Overall_Status = "Fail";
                              
                            }
                        }

                    }
                }

                return Overall_Status;
            }

            catch (Exception)
            {
                throw;
            }
        }
        //calculate status edit
        private string CalculateStatus(MainAuditSheetEdit objMT)
        {
            try
            {
                string Overall_Status = "";
                //int? Total = 0;
                int countoffields = (objMT.ListAuditMasterDetails).Count();

                for (int i = 0; i < (objMT.ListAuditMasterDetails).Count(); i++)
                {
                    if ((objMT.ListAuditMaster[0].Call_p1 == "Calling"))
                    {

                        if ((objMT.ListAuditMasterDetails[i].Metric == "FATAL ACCURACY") && (objMT.ListAuditMasterDetails[i].Parameter_Status == false))
                        {
                            Overall_Status = "Fail";
                            break;
                        }
                        else
                        {
                            if ((objMT.ListAuditMasterDetails[i].Metric == "FATAL ACCURACY") && (objMT.ListAuditMasterDetails[i].Parameter_Status == true))
                            {
                                Overall_Status = "Pass";
                            }
                        }
                    }
                    else
                    {
                        if ((objMT.ListAuditMasterDetails[i].FieldID != '2') && (objMT.ListAuditMasterDetails[i].Parameter_Status == true))
                        {
                            Overall_Status = "Pass";
                           
                        }
                        else
                        {
                            if ((objMT.ListAuditMasterDetails[i].FieldID != '2') && (objMT.ListAuditMasterDetails[i].Parameter_Status == false))
                            {
                                Overall_Status = "Fail";

                            }
                        }

                    }
                }
            

                return Overall_Status;
            }

            catch (Exception)
            {
                throw;
            }
        }


        // GET: TimeSheet
        public JsonResult ListofTrack()
        {
            try
            {
                var listofTrack = _ITrack.GetListofTrack();
                return Json(listofTrack, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult GetUser(string Username)
        {
            try
            {
                var listofuser = _IRegistration.GetUser( Username);
                var listofRM = _IRegistration.GetRM(Username);
                var listofMGR = _IRegistration.GetMGR(Username);
                var result = new { Agent = listofuser, RM = listofRM };
                return Json(new { Agent = listofuser,RM = listofRM ,MGR = listofMGR}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Getformsfields
        public JsonResult Getformsfields(int FormsCode)
        {
            try
            {

            IEnumerable<Formfields> Formfields  =  new List<Formfields>();

                using (var _context = new DatabaseContext())
                {

                    var listoffields = (from a in _context.AuditedForms.AsEnumerable()
                                        join b in _context.FormsFieldMaster on a.FieldID equals b.FieldID
                                        where a.FormsCode == FormsCode
                                        select b).ToList();
                    Formfields = listoffields.Select(x =>
                                new Formfields()
                                {
                                    
                                    Questions = x.Questions,
                                    Metric = x.Metric,
                                    Max_Score = x.Max_Score,
                                    Parameter_Status = x.Is_Selected,
                                });
                }
                ViewData["Data"] = Json(Formfields);
                return Json(Formfields, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public JsonResult ListofForms()
        {
            try
            {
                var listofforms = _IAuditForms.GetListofForms();

                return Json(listofforms, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Clculate Add final score
        [NonAction]
        private int? CalculateFinal_Score(AuditSheetModel AuditSheetModel)
         {
            try
            {
                int? val = 0;
                //int? Total = 0;
                int countoffields = (AuditSheetModel.Formfields).Count();

                for (int i = 0; i < (AuditSheetModel.Formfields).Count(); i++)
                {
                    if ((AuditSheetModel.Formfields[i].Parameter_Status == true))
                    {

                        val = val + AuditSheetModel.Formfields[i].Max_Score;

                    }   
                    }

                if ((AuditSheetModel.Call_p1 == "Non_Calling"))
                {
                    val = val + 10;

                }

                return val;
                }
                       
                catch (Exception)
                {
                    throw;
                }
            }

        //calculate edit final score
        [NonAction]
        private int? CalculateFinal_Score(MainAuditSheetEdit objMT)
        {
            try
            {
                int? val = 0;
                //int? Total = 0;
                int countoffields = (objMT.ListAuditMasterDetails).Count();

                for (int i = 0; i < (objMT.ListAuditMasterDetails).Count(); i++)
                {
                    if ((objMT.ListAuditMasterDetails[i].Parameter_Status == true))
                    {

                        val = val + objMT.ListAuditMasterDetails[i].Max_Score;
                    }

                  
                }

                if ((objMT.ListAuditMaster[0].Call_p1 == "Non_Calling") && (objMT.ListAuditMasterDetails[1].Parameter_Status == false))
                {
                    val = val + 10;

                }


                return val;
            }

            catch (Exception)
            {
                throw;
            }
        }

        //save audit sheet details Add
        [NonAction]
        private void SaveAuditSheetDetail(int FormsCode, int FieldID, string Questions,bool Parameter_Status, string Metric, int Max_Score,int Final_Score, int AuditSheetMasterID)
        {
            try
            {
            AuditSheetDetails objauditsheetdetails = new AuditSheetDetails();
            objauditsheetdetails.Audit_ID = 0;
            objauditsheetdetails.Audit_MasterID = AuditSheetMasterID;
            objauditsheetdetails.FormsCode = FormsCode;
            objauditsheetdetails.FieldID = FieldID;
                objauditsheetdetails.Questions = Questions;
            objauditsheetdetails.Parameter_Status = Parameter_Status;
            objauditsheetdetails.Metric = Metric;
            objauditsheetdetails.Max_Score = Max_Score;
                
            objauditsheetdetails.Final_Score = Final_Score;
            int Audit_ID = _IAuditSheet.AddAuditSheetDetail(objauditsheetdetails);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        //save audit sheet details edit
        [NonAction]
        private void SaveAuditSheetDetailEdit(int AuditID,int FormsCode, int FieldID, string Questions, bool Parameter_Status, string Metric, int Max_Score, int Final_Score, int AuditSheetMasterID)
        {
            try
            {
                AuditSheetDetails objauditsheetdetails = new AuditSheetDetails();
                objauditsheetdetails.Audit_ID = AuditID;
                objauditsheetdetails.Audit_MasterID = AuditSheetMasterID;
                objauditsheetdetails.FormsCode = FormsCode;
                objauditsheetdetails.FieldID = FieldID;
                objauditsheetdetails.Questions = Questions;
                objauditsheetdetails.Parameter_Status = Parameter_Status;
                objauditsheetdetails.Metric = Metric;
                objauditsheetdetails.Max_Score = Max_Score;

                objauditsheetdetails.Final_Score = Final_Score;
                int Audit_ID = _IAuditSheet.EditAuditSheetDetails(objauditsheetdetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Add Save
        public void Save(AuditSheetModel auditsheetmodel, int Audit_MasterID)
        {
        try
        {
            // foreach (var item in AuditSheetModel.Max_Score)
            for (int i = 0; i < (1); i++)
            {

                if ( (auditsheetmodel.FormsCode != 0))
                {
                    // auditsheetmodel.Formfields = new List<Formfields>();
                    //  var test = auditsheetmodel.Formfields[1].Questions;
                    #region Project 1
                    foreach (var item in auditsheetmodel.Formfields)
                    {
                        var FormsCode = auditsheetmodel.FormsCode;
                        var FieldID = item.FieldID;
                        var Questions = item.Questions;
                        var Parameter_Status = Convert.ToBoolean(item.Parameter_Status);
                        var Metric = item.Metric;
                        var Max_Score = item.Max_Score;
                        var Final_Score = 0;

                        if ((Parameter_Status == true) || (Parameter_Status == false && auditsheetmodel.Call_p1 == "Non_Calling" && FieldID == 2))
                        {
                            Final_Score = item.Max_Score;
                        }
                        else
                        {
                            Final_Score = 0;
                        }
                            SaveAuditSheetDetail(FormsCode, FieldID, Questions, Parameter_Status, Metric, Max_Score, Final_Score, Audit_MasterID);
                    }
                    #endregion
                }
            }
        }
        catch (Exception)
        {
            throw;
        }

    }

        //edit save
        public void Save(MainAuditSheetEdit objMT, int Audit_MasterID)
        {
            try
            {
                // foreach (var item in AuditSheetModel.Max_Score)
                for (int i = 0; i < (1); i++)
                {

                    if ((objMT.ListAuditMasterDetails[0].FormsCode != 0))
                    {
                        // auditsheetmodel.Formfields = new List<Formfields>();
                        //  var test = auditsheetmodel.Formfields[1].Questions;
                        #region Project 1
                        foreach (var item in objMT.ListAuditMasterDetails)
                        {
                            var AuditID = item.Audit_ID;
                            var FormsCode = item.FormsCode;
                            var FieldID = item.FieldID;
                            var Questions = item.Questions;
                            var Parameter_Status = Convert.ToBoolean(item.Parameter_Status);
                            var Metric = item.Metric;
                            var Max_Score = item.Max_Score;
                            var Final_Score = 0;

                            if ((Parameter_Status == true) || (Parameter_Status == false && objMT.ListAuditMaster[0].Call_p1 == "Non_Calling" && item.FieldID == 2))
                            {
                                Final_Score = item.Max_Score;
                            }
                            else
                            {
                                Final_Score = 0;
                            }
                            SaveAuditSheetDetailEdit(AuditID,FormsCode, FieldID, Questions, Parameter_Status, Metric, Max_Score, Final_Score, Audit_MasterID);
                        }
                        #endregion
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private AuditSheetAuditTB InsertAuditSheetAudit(int Audit_MasterID, int Status, string UserName)
        {
            try
            {
                AuditSheetAuditTB objAuditTB = new AuditSheetAuditTB();
                objAuditTB.Audit_AcceptanceID = 0;
                objAuditTB.Audit_MasterID = Audit_MasterID;
                objAuditTB.Created_On = DateTime.Now;
                objAuditTB.Auditor_ID = Convert.ToInt32(Session["Quality"]);
                objAuditTB.Acceptance_Status = Status;
                objAuditTB.User_Comments = string.Empty;
                objAuditTB.Audit_Date = DateTime.Now;
                objAuditTB.UserName = UserName;
                return objAuditTB;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //edit Audit tb acceptance table
       
        public JsonResult Delete(int Audit_MasterID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Audit_MasterID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var data = _IAuditSheet.DeleteAuditsheetByOnlyAudit_MasterID(Audit_MasterID);

                if (data > 0)
                {
                    return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /**
        public JsonResult CheckIsDateAlreadyUsed(DateTime FromDate)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(FromDate)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var data = _ITimeSheet.CheckIsDateAlreadyUsed(FromDate, Convert.ToInt32(Session["UserID"]));
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult Delete(int TimeSheetMasterID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(TimeSheetMasterID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

                var data = _ITimeSheet.DeleteTimesheetByTimeSheetMasterID(TimeSheetMasterID, Convert.ToInt32(Session["UserID"]));

                if (data > 0)
                {
                    return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

            private int ProjectSelectCount(TimeSheetModel timesheetmodel)
            {
                try
                {
                    int count = 0;
                    if (timesheetmodel.ProjectID1 != null && (timesheetmodel.texttotal_p1 != null && timesheetmodel.texttotal_p1 != 0))
                    {
                        count = count + 1;
                    }

                    if (timesheetmodel.ProjectID2 != null && (timesheetmodel.texttotal_p2 != null && timesheetmodel.texttotal_p2 != 0))
                    {
                        count = count + 1;
                    }

                    if (timesheetmodel.ProjectID3 != null && (timesheetmodel.texttotal_p3 != null && timesheetmodel.texttotal_p3 != 0))
                    {
                        count = count + 1;
                    }

                    if (timesheetmodel.ProjectID3 != null && (timesheetmodel.texttotal_p3 != null && timesheetmodel.texttotal_p3 != 0))
                    {
                        count = count + 1;
                    }

                    if (timesheetmodel.ProjectID4 != null && (timesheetmodel.texttotal_p4 != null && timesheetmodel.texttotal_p4 != 0))
                    {
                        count = count + 1;
                    }

                    if (timesheetmodel.ProjectID5 != null && (timesheetmodel.texttotal_p5 != null && timesheetmodel.texttotal_p5 != 0))
                    {
                        count = count + 1;
                    }

                    if (timesheetmodel.ProjectID6 != null && (timesheetmodel.texttotal_p6 != null && timesheetmodel.texttotal_p6 != 0))
                    {
                        count = count + 1;
                    }

                    return count;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            **/



        /**  public void SaveDescription(TimeSheetModel timesheetmodel, int TimeSheetMasterID)
          {
              try
              {

                  if (timesheetmodel.ProjectID1 != null)
                  {
                      InsertDescriptionDetail(timesheetmodel.ProjectID1, TimeSheetMasterID, timesheetmodel.Description_p1);
                  }

                  if (timesheetmodel.ProjectID2 != null)
                  {
                      InsertDescriptionDetail(timesheetmodel.ProjectID2, TimeSheetMasterID, timesheetmodel.Description_p2);
                  }

                  if (timesheetmodel.ProjectID3 != null)
                  {
                      InsertDescriptionDetail(timesheetmodel.ProjectID3, TimeSheetMasterID, timesheetmodel.Description_p3);
                  }

                  if (timesheetmodel.ProjectID4 != null)
                  {
                      InsertDescriptionDetail(timesheetmodel.ProjectID4, TimeSheetMasterID, timesheetmodel.Description_p4);
                  }

                  if (timesheetmodel.ProjectID5 != null)
                  {
                      InsertDescriptionDetail(timesheetmodel.ProjectID5, TimeSheetMasterID, timesheetmodel.Description_p5);
                  }

                  if (timesheetmodel.ProjectID6 != null)
                  {
                      InsertDescriptionDetail(timesheetmodel.ProjectID6, TimeSheetMasterID, timesheetmodel.Description_p6);
                  }
              }
              catch (Exception)
              {
                  throw;
              }

          } **/

        /**
            private TimeSheetAuditTB InsertTimeSheetAudit(int TimeSheetMasterID, int Status)
            {
                try
                {
                    TimeSheetAuditTB objAuditTB = new TimeSheetAuditTB();
                    objAuditTB.ApprovalTimeSheetLogID = 0;
                    objAuditTB.TimeSheetID = TimeSheetMasterID;
                    objAuditTB.Status = Status;
                    objAuditTB.CreatedOn = DateTime.Now;
                    objAuditTB.Comment = string.Empty;
                    objAuditTB.ApprovalUser = _IUsers.GetAdminIDbyUserID(Convert.ToInt32(Session["UserID"]));
                    objAuditTB.ProcessedDate = DateTime.Now;
                    objAuditTB.UserID = Convert.ToInt32(Session["UserID"]);
                    return objAuditTB;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            [NonAction]
            private void InsertDescriptionDetail(int? ProjectID, int TimeSheetMasterID, string Description)
            {
                try
                {
                    DescriptionTB objtimesheetdetails = new DescriptionTB();
                    objtimesheetdetails.DescriptionID = 0;
                    objtimesheetdetails.ProjectID = ProjectID;
                    objtimesheetdetails.UserID = Convert.ToInt32(Session["UserID"]);
                    objtimesheetdetails.CreatedOn = DateTime.Now;
                    objtimesheetdetails.TimeSheetMasterID = TimeSheetMasterID;
                    objtimesheetdetails.Description = Description;
                    int? TimeSheetID = _ITimeSheet.InsertDescription(objtimesheetdetails);
                }
                catch (Exception)
                {
                    throw;
                }
            }


**/
    }
}