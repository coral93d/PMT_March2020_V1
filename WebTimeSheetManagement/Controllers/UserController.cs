using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Interface;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateUserSession]
    public class UserController : Controller
    {
        private IAuditSheet _IAuditSheet;

        public UserController()
        {
            _IAuditSheet = new AuditSheetConcrete();
        }


        // GET: User
        [HttpGet]
        public ActionResult Dashboard()
        {
            var AuditSheetResult = _IAuditSheet.GetAuditSheetsCountByUserID(Convert.ToString(Session["UserID"]));

            if (AuditSheetResult != null)
            {
                ViewBag.SubmittedAuditsheetCount = AuditSheetResult.SubmittedCount;
                ViewBag.ApprovedAuditsheetCount = AuditSheetResult.ApprovedCount;
                ViewBag.AppealedAuditsheetCount = AuditSheetResult.AppealCount;
                ViewBag.ShowAllAuditsheetCount = AuditSheetResult.ShowAll;
            }
            else
            {
                ViewBag.SubmittedAuditsheetCount = 0;
                ViewBag.ApprovedAuditsheetCount = 0;
                ViewBag.AppealedAuditsheetCount = 0;
                ViewBag.ShowAllAuditsheetCount = 0;
            }


                return View();
        }

    }
}