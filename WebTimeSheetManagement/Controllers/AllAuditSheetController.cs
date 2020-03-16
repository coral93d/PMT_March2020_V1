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
   
    public class AllAuditSheetController : Controller
    {
        IProject _IProject;
        ITimeSheet _ITimeSheet;
        IAuditSheet _IAuditSheet;
        public AllAuditSheetController()
        {
            _IProject = new ProjectConcrete();
            _ITimeSheet = new TimeSheetConcrete();
            _IAuditSheet = new AuditSheetConcrete();
        }
        public ActionResult AuditSheet()
        {
            return View();
        }        
        public ActionResult LoadAuditSheetData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;
                var v = _IAuditSheet.ShowAuditSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["Quality"]));
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult AuditSheet1()
        {
            return View();
        }
        public ActionResult LoadAuditSheetData1()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;
                var v = _IAuditSheet.ShowAuditSheet_QALead(sortColumn, sortColumnDir, searchValue);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }
        public ActionResult Details(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("AuditSheet", "AllAuditSheet");
                }

                MainAuditSheetView objMT = new MainAuditSheetView();
                objMT.ListAuditSheetDetails = _IAuditSheet.AuditsheetDetailsbyAuditSheetMasterID(Convert.ToInt32(id));
                objMT.ListAuditSheetDetailsforms = _IAuditSheet.AuditsheetDetailsviewbyAuditSheetMasterID(Convert.ToInt32(id));
                objMT.ListAppealSheetDetails = _IAuditSheet.CommentsbyAuditSheetMasterID(Convert.ToInt32(id));
                objMT.Audit_MasterID = Convert.ToInt32(id);
                return View(objMT);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public ActionResult AllAppeal()
        {
            return View();
        }
        public ActionResult LoadAllAppealData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var v = _IAuditSheet.ShowAllAppeal(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["Quality"]));
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult AllAppeal1()
        {
            return View();
        }
        public ActionResult LoadAllAppealData_Lead()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var v = _IAuditSheet.ShowAllAppeal_Lead(sortColumn, sortColumnDir, searchValue);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult SubmittedAuditSheet()
        {
            return View();
        }
        public ActionResult LoadSubmittedAuditSheetData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;
                var v = _IAuditSheet.ShowAuditSheetStatus(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["Quality"]), 1);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult SubmittedAuditSheet1()
        {
            return View();
        }
        public ActionResult LoadSubmittedAuditSheetData_Lead()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;
                var v = _IAuditSheet.ShowSubmittedAuditSheet_Lead(sortColumn, sortColumnDir, searchValue, 1);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult ApprovedAuditSheet()
        {
            return View();
        }
        public ActionResult LoadApprovedAuditSheetData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var v = _IAuditSheet.ShowApprovedAuditSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["Quality"]), 2);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult ApprovedAuditSheet1()
        {
            return View();
        }
        public ActionResult LoadApprovedAuditSheetData_Lead()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var v = _IAuditSheet.ShowApprovedAuditSheet_Lead(sortColumn, sortColumnDir, searchValue, 2);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult AppealedAuditSheet()
        {
            return View();
        }        
        public ActionResult LoadAppealedAuditSheetData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var v = _IAuditSheet.ShowAppealedAuditSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["Quality"]), 3);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult AppealedAuditSheet1()
        {
            return View();
        }
        public ActionResult LoadAppealedAuditSheetData1()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var v = _IAuditSheet.ShowAppealedAuditSheet_Lead(sortColumn, sortColumnDir, searchValue,3);
                recordsTotal = v.Count();
                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("AuditSheet", "AllAuditSheet");
                }

                MainAuditSheetView objMT = new MainAuditSheetView();
                objMT.ListAuditSheetDetails = _IAuditSheet.AuditsheetDetailsbyAuditSheetMasterID(Convert.ToInt32(id));
                objMT.ListAuditSheetDetailsforms = _IAuditSheet.AuditsheetDetailsviewbyAuditSheetMasterID(Convert.ToInt32(id));
                objMT.ListAppealSheetDetails = _IAuditSheet.CommentsbyAuditSheetMasterID(Convert.ToInt32(id));

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
        public ActionResult Edit(MainAuditSheetView objMT)
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

                    var AuditSheetStatus = 2;
                    var Final_Status = CalculateFinal_Score(objMT).ToString();
                    var Overall_Status = CalculateStatus(objMT);
                    var Audit_MasterID = objMT.ListAuditSheetDetailsforms[0].Audit_MasterID;
                    var Auditor_Comments = objMT.Auditor_Comments;
                    var Appeal_Status = '2';
                    //  var Formfields = auditsheetmodel.Formfields.ToList(); 
                    AuditSheetDetails objauditsheetdetails = new AuditSheetDetails();


                    _IAuditSheet.UpdateAuditSheetStatusAppeal(Audit_MasterID, AuditSheetStatus, Final_Status, Overall_Status, Auditor_Comments, Appeal_Status.ToString());


                    foreach (var item in objMT.ListAuditSheetDetailsforms)
                    {

                        var Audit_ID = item.Audit_ID;
                        var AuditSheetMasterID = item.Audit_MasterID;
                        var FormsCode = item.FormsCode;
                        var FieldID = Convert.ToInt32(item.FieldID);
                        var Questions = item.Questions;
                        var Parameter_Status = item.Parameter_Status;
                        var Metric = item.Metric;
                        var Max_Score = item.Max_Score;


                        var Final_Score = 0;

                        if ((Parameter_Status == true) || (Parameter_Status == false && objMT.ListAuditSheetDetails[0].Call_p1 == "Non_Calling" && item.FieldID == 2))
                        {
                            Final_Score = item.Max_Score;
                        }
                        else
                        {
                            Final_Score = 0;
                        }
                        // objauditsheetdetails.Final_Score = Final_Score;
                        SaveAuditSheetDetail(Audit_ID, FormsCode, FieldID, Questions, Parameter_Status, Metric, Max_Score, Final_Score, AuditSheetMasterID);

                        //this.Text = "22/11/2009";
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

        public ActionResult RejectAppeal(AuditSheetAppeal AuditSheetAppeal)
        {
            try
            {
                if (AuditSheetAppeal.Auditor_Comments == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(AuditSheetAppeal.Audit_MasterID)))
                {
                    return Json(false);
                }
                _IAuditSheet.UpdateAppeal(AuditSheetAppeal, 2); //Appeal
                
                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }
        }
 
        [NonAction]
        private void SaveAuditSheetDetail(int Audit_ID, int FormsCode, int FieldID, string Questions, bool Parameter_Status, string Metric, int Max_Score, int Final_Score, int AuditSheetMasterID)
        {
            try
            {
                AuditSheetDetails objauditsheetdetails = new AuditSheetDetails();
                objauditsheetdetails.Audit_ID = Audit_ID;
                objauditsheetdetails.Audit_MasterID = AuditSheetMasterID;
                objauditsheetdetails.FormsCode = FormsCode;
                objauditsheetdetails.FieldID = FieldID;
                objauditsheetdetails.Questions = Questions;
                objauditsheetdetails.Parameter_Status = Parameter_Status;
                objauditsheetdetails.Metric = Metric;
                objauditsheetdetails.Max_Score = Max_Score;
                objauditsheetdetails.Final_Score = Final_Score;

                int Audit_IDs = _IAuditSheet.EditAuditSheetMaster(objauditsheetdetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [NonAction]
        private int? CalculateFinal_Score(MainAuditSheetView objMT)
        {
            try
            {
                int? val = 0;                
                int countoffields = (objMT.ListAuditSheetDetailsforms).Count();

                for (int i = 0; i < (objMT.ListAuditSheetDetailsforms).Count(); i++)
                {
                    if ((objMT.ListAuditSheetDetailsforms[i].Parameter_Status == true))
                    {
                        val = val + objMT.ListAuditSheetDetailsforms[i].Max_Score;
                    }
                }
                if ((objMT.ListAuditSheetDetails[0].Call_p1 == "Non_Calling") && (objMT.ListAuditSheetDetailsforms[1].Parameter_Status == false))
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

        [NonAction]
        private string CalculateStatus(MainAuditSheetView objMT)
        {
            try
            {
                string Overall_Status = "";                
                int countoffields = (objMT.ListAuditSheetDetailsforms).Count();
                for (int i = 0; i < (objMT.ListAuditSheetDetailsforms).Count(); i++)
                {
                    if ((objMT.ListAuditSheetDetails[0].Call_p1 == "Calling"))
                    {

                        if ((objMT.ListAuditSheetDetailsforms[i].Metric == "FATAL ACCURACY") && (objMT.ListAuditSheetDetailsforms[i].Parameter_Status == false))
                        {
                            Overall_Status = "Fail";
                            break;
                        }
                        else
                        {
                            if ((objMT.ListAuditSheetDetailsforms[i].Metric == "FATAL ACCURACY") && (objMT.ListAuditSheetDetailsforms[i].Parameter_Status == true))
                            {
                                Overall_Status = "Pass";
                            }
                        }
                    }

                    else
                    {
                        if ((objMT.ListAuditSheetDetailsforms[i].FieldID != '2') && (objMT.ListAuditSheetDetailsforms[i].Parameter_Status == true))
                        {
                            Overall_Status = "Pass";

                        }
                        else
                        {
                            if ((objMT.ListAuditSheetDetailsforms[i].FieldID != '2') && (objMT.ListAuditSheetDetailsforms[i].Parameter_Status == false))
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


      

    }
}