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
    public class ShowAllAuditSheetController : Controller
    {

        IProject _IProject;
        IUsers _IUsers;
        ITimeSheet _ITimeSheet;
        IAuditSheet _IAuditSheet;
        public ShowAllAuditSheetController()
        {
            _IProject = new ProjectConcrete();
            _ITimeSheet = new TimeSheetConcrete();
            _IAuditSheet = new AuditSheetConcrete();
            _IUsers = new UsersConcrete();
        }

        // GET: ShowAllTimeSheet
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

                var auditsheetdata = _IAuditSheet.ShowAuditSheetuser(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]));
                recordsTotal = auditsheetdata.Count();
                var data = auditsheetdata.Skip(skip).Take(pageSize).ToList();

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
                // objMT.ListofProjectNames = _ITimeSheet.GetProjectNamesbyTimeSheetMasterID(Convert.ToInt32(id));
                // objMT.ListofPeriods = _ITimeSheet.GetPeriodsbyTimeSheetMasterID(Convert.ToInt32(id));
                // objMT.ListoDayofWeek = DayofWeek();
                objMT.Audit_MasterID = Convert.ToInt32(id);
                return View(objMT);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Approval(AuditSheetApproval AuditSheetApproval)
        {
            try
            {
                if (AuditSheetApproval.User_Comments == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(AuditSheetApproval.Audit_MasterID)))
                {
                    return Json(false);
                }

                _IAuditSheet.UpdateAuditSheetStatus(AuditSheetApproval, 2); //Approve

                if (_IAuditSheet.IsAuditsheetALreadyProcessed(AuditSheetApproval.Audit_MasterID))
                {
                    _IAuditSheet.UpdateAuditSheetAuditStatus(AuditSheetApproval.Audit_MasterID, AuditSheetApproval.User_Comments, 2.ToString());
                }

                return Json(true);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Rejected(AuditSheetApproval AuditSheetApproval)
        {
            try
            {
                if (AuditSheetApproval.User_Comments == null)
                {
                    return Json(false);
                }

                if (string.IsNullOrEmpty(Convert.ToString(AuditSheetApproval.Audit_MasterID)))
                {
                    return Json(false);
                }
                // AuditSheetAppeal auditSheetAppeal =AuditSheetApproval;

                _IAuditSheet.UpdateAuditSheetStatus(AuditSheetApproval, 3); //Appeal
                _IAuditSheet.UpdateAuditSheetAppeal(AuditSheetApproval, 3.ToString(), 1.ToString());

                if (_IAuditSheet.IsAuditsheetALreadyProcessed(AuditSheetApproval.Audit_MasterID))
                {
                    _IAuditSheet.UpdateAuditSheetAuditStatus(AuditSheetApproval.Audit_MasterID, AuditSheetApproval.User_Comments, 3.ToString());
                }

                // else
                // {
                // _ITimeSheet.InsertTimeSheetAuditLog(InsertAuditSheetAudit(AuditSheetApproval, 3));
                // }


                return Json(true);
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

        public ActionResult ApprovedAuditSheet()
        {
            return View();
        }

        public ActionResult AppealedAuditSheet()
        {
            return View();
        }


        public ActionResult AllAppeal()
        {
            return View();
        }
        public ActionResult LoadSubmittedTData()
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

                var auditsheetdata = _IAuditSheet.ShowAllSubmittedAuditSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]));
                recordsTotal = auditsheetdata.Count();
                var data = auditsheetdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadRejectedData()
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

                var auditsheetdata = _IAuditSheet.ShowAllAppealedAuditSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]));
                recordsTotal = auditsheetdata.Count();
                var data = auditsheetdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public ActionResult LoadApprovedData()
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

                var auditsheetdata = _IAuditSheet.ShowAllApprovedAuditSheet(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]));
                recordsTotal = auditsheetdata.Count();
                var data = auditsheetdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }


        public ActionResult LoadAppealData()
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

                var auditsheetdata = _IAuditSheet.ShowAllAppealuser(sortColumn, sortColumnDir, searchValue, Convert.ToInt32(Session["UserID"]));
                recordsTotal = auditsheetdata.Count();
                var data = auditsheetdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }



        /**
             [NonAction]
             public List<string> DayofWeek()
             {
                 List<string> li = new List<string>();
                 li.Add("Sunday");
                 li.Add("Monday");
                 li.Add("Tuesday");
                 li.Add("Wednesday");
                 li.Add("Thursday");
                 li.Add("Friday");
                 li.Add("Saturday");
                 li.Add("Total");
                 return li;
             }
           **/

        /** Need to check
                private AuditSheetAuditTB InsertAuditSheetAudit(AuditSheetApproval AuditSheetApproval, int Status)
                {
                    try
                    {
                        AuditSheetAuditTB objAuditTB = new AuditSheetAuditTB();
                        objAuditTB.Audit_AcceptanceID = 0;
                        objAuditTB.Audit_MasterID = AuditSheetApproval.Audit_MasterID;
                        objAuditTB.UserName = _IUsers.GetUserIDbyTimesheetID(AuditSheetApproval.Audit_MasterID).ToString();
                        objAuditTB.Created_On = DateTime.Now;
                        objAuditTB.User_Comments = AuditSheetApproval.User_Comments;
                        objAuditTB.Acceptance_Status = Status;
                        objAuditTB.Audit_Date = AuditSheetApproval.
                        objAuditTB.ApprovalUser = Convert.ToInt32(Session["AdminUser"]);
                        objAuditTB.ProcessedDate = DateTime.Now;
                      //  objAuditTB.UserID = _IUsers.GetUserIDbyTimesheetID(TimeSheetApproval.TimeSheetMasterID);
                        return objAuditTB;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                    **/


        /**

      public ActionResult Rejected(TimeSheetApproval TimeSheetApproval)
      {
          try
          {
              if (TimeSheetApproval.Comment == null)
              {
                  return Json(false);
              }

              if (string.IsNullOrEmpty(Convert.ToString(TimeSheetApproval.TimeSheetMasterID)))
              {
                  return Json(false);
              }

              _ITimeSheet.UpdateTimeSheetStatus(TimeSheetApproval, 3); //Reject

              if (_ITimeSheet.IsTimesheetALreadyProcessed(TimeSheetApproval.TimeSheetMasterID))
              {
                  _ITimeSheet.UpdateTimeSheetAuditStatus(TimeSheetApproval.TimeSheetMasterID, TimeSheetApproval.Comment, 3);
              }
              else
              {
                  _ITimeSheet.InsertTimeSheetAuditLog(InsertTimeSheetAudit(TimeSheetApproval, 3));
              }


              return Json(true);
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

              var data = _ITimeSheet.DeleteTimesheetByOnlyTimeSheetMasterID(TimeSheetMasterID);

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
  **/





    }
}