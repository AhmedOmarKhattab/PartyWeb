using party.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace party.Controllers
{
    public class adminController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();
        // GET: admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult addcoor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addcoor(coordinator coordinator, HttpPostedFileBase imgfile)
        {
            if (coordinator.name != null && coordinator.phone != null)
            {
                string path = uploadimgfile(imgfile);
                coordinator.image = path;
              
                db.coordinators.Add(coordinator);
                db.SaveChanges();
                return RedirectToAction("coorList");
            }
            else
            {
                TempData["Message"] = "أملا جميع الحقول";
                return RedirectToAction("addcoor");


            }

        }
        public string uploadimgfile(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();

            string extension = Path.GetExtension(file.FileName);


            path = Path.Combine(Server.MapPath("~/img"), random + Path.GetFileName(file.FileName));
            file.SaveAs(path);
            path = "~/img/" + random + Path.GetFileName(file.FileName);
            return path;
        }
        public ActionResult coorList()
        {
            return View(db.coordinators.ToList());
        }
        public ActionResult addDecorate(int ?id)
        {
            decoration n = new decoration();
            n.CoorId = id;

            return View(n);
        
        }
        [HttpPost]
        public ActionResult addDecorate(decoration decoration, HttpPostedFileBase imgfile, HttpPostedFileBase imgfileone)
        {
            if (decoration.decoreName != null && decoration.des != null && decoration.cost != null)
            {
                string path = uploadimgfile(imgfile);
                string pathTwo = uploadimgfile(imgfileone);

                decoration.image = path;
                decoration.imageTwo = pathTwo;
                db.decorations.Add(decoration);
                db.SaveChanges();
                TempData["Message"] = "تم اضافة البيانات بنجاح";

                return RedirectToAction("addDecorate", new { /*name=decoration.CoorName */});
            }
            else
            {
                TempData["Message"] = "أملا جميع الحقول";
                return RedirectToAction("addDecorate");


            }

        }
        public ActionResult decorationList()
        {
            return View(db.decorations.ToList());
        }
        public ActionResult CustomerBooking(int? coorId)
        {
            ViewBag.Coors=db.decorations.ToList();
            return View(db.Requests
                .Where(c => !coorId.HasValue || c.decoreId == coorId)
                .OrderByDescending(c=>c.Id)
                .ToList());
        }
        public ActionResult SpecialRequest()
        {
            return View(db.SpecialRequests.Include("coordinator").ToList());
        }
        public ActionResult DelCoor(int id)
        {
            try
            {
                var coor = db.coordinators.Where(x => x.Id == id).SingleOrDefault();
                db.coordinators.Remove(coor);
                db.SaveChanges();
                TempData["Message"] = "تم حذف البيانات بنجاح";

                return RedirectToAction("coorList");

            }
            catch
            {
                TempData["Message"] = "لا يمكن حذف المنسق فم بحذف القاعات الخاصة به اولا";


                return RedirectToAction("coorList");

            }

        }
        public ActionResult DelDecore(int id)
        {
            try
            {
                var decore = db.decorations.Where(x => x.Id == id).SingleOrDefault();
                db.decorations.Remove(decore);
                db.SaveChanges();
                TempData["Message"] = "تم حذف البيانات بنجاح";

                return RedirectToAction("decorationList");

            }
            catch
            {
                TempData["Message"] = "لا يمكن حذف المنسق قم بحذ او الغاء الطلبيات على هذه القاعة اولا";


                return RedirectToAction("decorationList");

            }

        }
        public ActionResult DelReq(int id)
        {
            try
            {
                var req = db.SpecialRequests.Where(x => x.Id == id).SingleOrDefault();
                db.SpecialRequests.Remove(req);
                db.SaveChanges();
                TempData["Message"] = "تم حذف البيانات بنجاح";

                return RedirectToAction("SpecialRequest");

            }
            catch
            {
                TempData["Message"] = "فشل الحذف";


                return RedirectToAction("SpecialRequest");

            }
        }
        public ActionResult DelBook(int id)
        {
            try
            {
                var req = db.Requests.Where(x => x.Id == id).SingleOrDefault();
                db.Requests.Remove(req);
                db.SaveChanges();
                TempData["Message"] = "تم حذف البيانات بنجاح";

                return RedirectToAction("CustomerBooking");

            }
            catch
            {
                TempData["Message"] = "فشل الحذف";


                return RedirectToAction("CustomerBooking");

            }

        }
    }
}