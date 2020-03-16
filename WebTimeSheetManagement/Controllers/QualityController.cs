using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Interface;

namespace WebTimeSheetManagement.Controllers
{
    public class QualityController : Controller
    {
        // GET: Quality
        private IAuditSheet _IAuditSheet;      

        public QualityController()
        {
            _IAuditSheet = new AuditSheetConcrete();           
        }
        // GET: Admin
        [HttpGet]
        public ActionResult Dashboard()
        {
            try
            {
              var AuditSheetResult = _IAuditSheet.GetAuditSheetsCountByQualityID(Convert.ToString(Session["Quality"]));

                if (AuditSheetResult != null)
                {
                    ViewBag.SubmittedAuditsheetCount = AuditSheetResult.SubmittedCount;
                    ViewBag.ApprovedAuditsheetCount = AuditSheetResult.ApprovedCount;
                    ViewBag.AppealedAuditsheetCount = AuditSheetResult.AppealCount;
                    ViewBag.ShowAllAuditsheetCount = AuditSheetResult.ShowAll;
                    ViewBag.less48 = AuditSheetResult.less48;
                    ViewBag.Greater48 = AuditSheetResult.Greater48;
                    ViewBag.Within24 = AuditSheetResult.Within24;
                }
                else
                {
                    ViewBag.SubmittedAuditsheetCount = 0;
                    ViewBag.ApprovedAuditsheetCount = 0;
                    ViewBag.AppealedAuditsheetCount = 0;
                    ViewBag.ShowAllAuditsheetCount = 0;
                    ViewBag.less48 = 0;
                    ViewBag.Greater48 = 0;
                    ViewBag.Within24 = 0;
                }                

                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }


      
    }
}