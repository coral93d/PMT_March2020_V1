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
    
    public class TrackController : Controller
    {
        ITrack _ITrack;
        public TrackController()
        {
            _ITrack = new TrackConcrete();
        }

        // GET: Project
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

      
        public JsonResult CheckTrackNameExists(string TrackName)
        {
            try
            {
                var isTrackNameExists = false;

                if (TrackName != null)
                {
                    isTrackNameExists = _ITrack.CheckTrackNameExists(TrackName);
                }

                if (isTrackNameExists)
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
        public ActionResult Add(TrackMaster TrackMaster)
        {
            if (ModelState.IsValid)
            {
                var result = _ITrack.SaveTrack(TrackMaster);

                if (result > 0)
                {
                    TempData["TrackMessage"] = "Track Added Successfully";
                    ModelState.Clear();
                    return RedirectToAction("Add");
                }
            }

            return View(TrackMaster);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add_Form(TrackMaster TrackMaster)
        {
            if (ModelState.IsValid)
            {
                var result = _ITrack.SaveTrack(TrackMaster);

                if (result > 0)
                {
                    TempData["TrackMessage"] = "Track Added Successfully";
                    ModelState.Clear();
                    return RedirectToAction("Add");
                }
            }

            return View(TrackMaster);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadTrackData()
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

                var Trackdata = _ITrack.ShowTrack(sortColumn, sortColumnDir, searchValue);
                recordsTotal = Trackdata.Count();
                var data = Trackdata.Skip(skip).Take(pageSize).ToList();

                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception)
            {
                throw;
            }

        }

        public JsonResult Delete(string TrackID)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(TrackID)))
                {
                    return Json("Error", JsonRequestBehavior.AllowGet);
                }

               // var isExistsinTimesheet = _IProject.CheckProjectIDExistsInTimesheet(Convert.ToInt32(ProjectID));

             //   var isExistsinExpense = _IProject.CheckProjectIDExistsInExpense(Convert.ToInt32(ProjectID));

               // if (isExistsinTimesheet == false && isExistsinExpense == false)
               // {
                    var data = _ITrack.TrackDelete(Convert.ToInt32(TrackID));

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
             //   {
                   // return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
               // }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}