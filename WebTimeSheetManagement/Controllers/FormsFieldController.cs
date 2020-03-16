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
    [ValidateSuperAdminSession]
    public class FormsFieldController : Controller
    {
        IFormsField _IFormsField;
        public FormsFieldController()
        {
                _IFormsField = new FormsFieldConcrete();
        }

        // GET: Project
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

    /**    public JsonResult CheckProjectCodeExists(string ProjectCode)
        {
            try
            {
                var isProjectCodeExists = false;

                if (ProjectCode != null)
                {
                    isProjectCodeExists = _IProject.CheckProjectCodeExists(ProjectCode);
                }

                if (isProjectCodeExists)
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
    **/

        public JsonResult CheckFormsFieldNameExists(string Questions)
        {
            try
            {
                var isFormsFieldNameExists = false;

                if (Questions != null)
                {
                    isFormsFieldNameExists = _IFormsField.CheckFormsFieldNameExists(Questions);
                }

                if (isFormsFieldNameExists)
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
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormsFieldMaster FormsFieldMaster)
        {
            if (ModelState.IsValid)
            {
                var result = _IFormsField.SaveFormsField(FormsFieldMaster);

                if (result > 0)
                {
                    TempData["FormsFieldMessage"] = "FormsField Added Successfully";
                    ModelState.Clear();
                    return RedirectToAction("Add");
                }
            }

            return View(FormsFieldMaster);
        }









        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Form(FormsFieldMaster FormsFieldMaster)
        {
            if (ModelState.IsValid)
            {
                var result = _IFormsField.SaveFormsField(FormsFieldMaster);

                if (result > 0)
                {
                    TempData["FormsFieldMessage"] = "Project Added Successfully";
                    ModelState.Clear();
                    return RedirectToAction("Add");
                }
            }

            return View(FormsFieldMaster);
        }







        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadFormsFieldData()
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

                var FormsFielddata = _IFormsField.ShowFormsField(sortColumn, sortColumnDir, searchValue);
                recordsTotal = FormsFielddata.Count();
                var data = FormsFielddata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

      public JsonResult Delete(int FieldID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(FieldID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

               // var isExistsinTimesheet = _IFormsField.CheckProjectIDExistsInTimesheet(Convert.ToInt32(ProjectID));

               // var isExistsinExpense = _IProject.CheckProjectIDExistsInExpense(Convert.ToInt32(ProjectID));

                //if (isExistsinTimesheet == false && isExistsinExpense == false)
               // {
                    var data = _IFormsField.FormsFieldDelete(Convert.ToInt32(FieldID));

                    if (data > 0)
                    {
                        return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                    }
              //  }
               // else
               // {
                   // return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

  
    }
}