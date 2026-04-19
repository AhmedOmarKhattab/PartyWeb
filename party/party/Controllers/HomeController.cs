using party.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace party.Controllers
{
    public class HomeController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();

        public ActionResult Index()
        {
            allProject n = new allProject();
            n.decorations = db.decorations.Include("coordinator").Take(3).ToList();
            n.coordinators = db.coordinators.ToList();

            return View(n);
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

        public ActionResult CoorList()
        {
            allProject n = new allProject();
            n.decorations = db.decorations.Include("coordinator").ToList();

            return View(n);
        }
        public ActionResult Decorationdet(int?id)
        {
            allProject n = new allProject();
            n.decoration = db.decorations.Where(x => x.Id == id).SingleOrDefault();

            return View(n);
        }
        public ActionResult Service()
        {

            return View();
        }
        public ActionResult AboutUs()
        {

            return View();
        }
        public ActionResult yourDecore()
        {
            SpecialRequestModel v = new SpecialRequestModel();
            v.coordinatorsList = db.coordinators.ToList();
            return View(v);
        }
        [HttpPost]
        public ActionResult yourDecore(SpecialRequestModel b, HttpPostedFileBase imgfile)
        {

            SpecialRequest n = new SpecialRequest();
            try
            {
                if (b.monsba != null && b.ClientName != null && b.ClientPhone != null)
                {
                    string image = uploadimgfile(imgfile);
                    n.date = b.date;
                    n.ClientName = b.ClientName;
                    n.ClientPhone = b.ClientPhone;
                    n.ClientAdress = b.ClientAdress;
                    n.monsba = b.monsba;
                    n.image = image;
                    n.des = b.des;
                    n.CoorId = b.coordinatorObject.Id;
                    var coor = db.coordinators.Where(x => x.Id == b.coordinatorObject.Id).SingleOrDefault();

                    db.SpecialRequests.Add(n);
                    db.SaveChanges();
                    TempData["Message"] = "تم ارسال طلب حجز خاص لقاعة سيقوم المنسق '"+coor.name+"'" +"بتواصل معاك من الرقم الاتى" + "'"+coor.phone+"'";

                    return RedirectToAction("yourDecore");

                    }
                }
                    catch
            {
                TempData["Message"] = "فشل الحجز";

            }

                return RedirectToAction("yourDecore");
        }
        public ActionResult BookDecore(int decoreId)
        {
            RequestModel b = new RequestModel();
            b.decoreId = decoreId;
            return View(b);

        }
        [HttpPost]
        public ActionResult BookDecore(RequestModel special)
        {
            try
            {
                if ( special.ClientName != null && special.ClientPhone != null)
                {
                    Request m = new Request();
                    m.decoreId = special.decoreId;
                    m.ClientAdress = special.ClientAdress;
                    m.ClientName = special.ClientName;
                    m.ClientPhone = special.ClientPhone;
                    m.monsba = special.monsba;
                    m.date = special.date;
                    m.decoreId = special.decoreId;
                    db.Requests.Add(m);
                    db.SaveChanges();
                    TempData["Message"] = "تم ارسال طلب حجز قاعه";

                    return RedirectToAction("BookDecore", new { decoreId = special.decoreId });

                }
            }
            catch
            {
                TempData["Message"] = "فشل الحجز";

            }

            return RedirectToAction("BookDecore", new { decoreId = special.decoreId });

        }
      

    }
}