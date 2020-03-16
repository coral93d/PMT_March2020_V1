using EventApplicationCore.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebTimeSheetManagement.Concrete;
using WebTimeSheetManagement.Filters;
using WebTimeSheetManagement.Helpers;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;

namespace WebTimeSheetManagement.Controllers
{
    [ValidateSuperAdminSession]
    public class AuditFormsController : Controller
    {
        private IRegistration _IRegistration;
        private IRoles _IRoles;
        private IAssignRoles _IAssignRoles;
        private ICacheManager _ICacheManager;
        private IUsers _IUsers;
        private IProject _IProject;
        private IAuditForms _IAuditForms;


        public AuditFormsController()
        {
            _IRegistration = new RegistrationConcrete();
            _IRoles = new RolesConcrete();
            _IAssignRoles = new AssignRolesConcrete();
            _ICacheManager = new CacheManager();
            _IUsers = new UsersConcrete();
            _IProject = new ProjectConcrete();
            _IAuditForms = new AuditFormsConcrete();
        }

        // GET: SuperAdmin
   


        [HttpGet]
        public ActionResult AuditForms()
        {
            try
            {
                AuditFormsModel auditFormsModel = new AuditFormsModel();
                auditFormsModel.ListofFormsField = _IAuditForms.ListofFormsField();
            

                return View(auditFormsModel);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult CheckFormsCodeExists(int FormsCode)
        {
            try
            {
                var isFormsCodeExists = false;

                if (FormsCode != 0)
                {
                    isFormsCodeExists = _IAuditForms.CheckFormsCodeExists(FormsCode);
                }

                if (isFormsCodeExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    

        public JsonResult CheckFormNameExists(string FormName)
        {
            try
            {
                var isFormNameExists = false;

                if (FormName != null)
                {
                    isFormNameExists = _IAuditForms.CheckFormNameExists(FormName);
                }

                if (isFormNameExists)
                {
                    return Json(data: true);
                }
                else
                {
                    return Json(data: false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public ActionResult AuditForms(AuditFormsModel objassign)
        {
            try
            {
                if (objassign.ListofFormsField == null)
                {
                    TempData["MessageErrorRoles"] = "There are no form fields to add";
                    objassign.ListofFormsField = _IAuditForms.ListofFormsField();
                   // objassign.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
                    return View(objassign);
                }

               

                var SelectedCount = (from User in objassign.ListofFormsField
                                     where User.selectedFormsField == true
                                     select User).Count();

           

                if ((SelectedCount == 0))
                {
                    TempData["MessageErrorRoles"] = "You have not Selected any Fields";
                    objassign.ListofFormsField = _IAuditForms.ListofFormsField();
                  //  objassign.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
                   // objassign.ListofQuality = _IAssignRoles.GetListofUnAssignedQuality();
                    return View(objassign);
                }


                if (ModelState.IsValid)
                {
                    objassign.CreatedBy = Convert.ToInt32(Session["SuperAdmin"]);
                    objassign.FormsID = 12;
                  //  objassign.CreatedOn = DateTime.Now;
                   // objassign.FieldID = 1;
                    _IAuditForms.SaveAuditForms(objassign);
                    TempData["MessageRoles"] = "Form Created Successfully!";
                }

                objassign = new AuditFormsModel();

                objassign.ListofFormsField = _IAuditForms.ListofFormsField();
              //  objassign.ListofUser = _IAssignRoles.GetListofUnAssignedUsers();
              //  objassign.ListofQuality = _IAssignRoles.GetListofUnAssignedQuality();

                return RedirectToAction("AuditForms");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetListofForms()
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

                var AuditFormsdata = _IAuditForms.ShowForms(sortColumn, sortColumnDir, searchValue);
                recordsTotal = AuditFormsdata.Count();
                var data = AuditFormsdata.Skip(skip).Take(pageSize).Distinct().ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}