using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mertaksublogg.Models;
using PagedList;
using PagedList.Mvc;


namespace mertaksublogg.Controllers
{
    public class HomeController : Controller
    {
        mertaksubloggDB db = new mertaksubloggDB();
        // GET: Home
        public ActionResult Index(int? i)
        {
            var makales = db.makales.OrderByDescending(m => m.makaleid).ToList().ToPagedList(i ?? 1, 7);
            return View(makales);
        }
        public ActionResult PopulerPartial()
        {
            return View(db.makales.OrderByDescending(m => m.okunma).Take(5));
        }
        public ActionResult EditorPartial()
        {
            return View(db.makales.OrderByDescending(m => m.uyeid == 2).Take(5));
        }
        public ActionResult BuyukResimPartial()
        {
            return View();
        }
        public ActionResult MakaleDetay(int id)
        {
            var makale = db.makales.Where(m => m.makaleid == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }
        public ActionResult OkunmaArttir(int Makaleid)
        {
            var makale = db.makales.Where(m => m.makaleid == Makaleid).SingleOrDefault();
            makale.okunma += 1;
            db.SaveChanges();
            return View();
        }
        public JsonResult YorumYap(string yorum, int makaleid)
        {

            var uyeid = Session["uyeid"];
            if (yorum == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);


            }
            db.yorums.Add(new yorum { uyeid = Convert.ToInt32(uyeid), makaleid = makaleid, icerik = yorum, tarih = DateTime.Now });
            db.SaveChanges();
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        public ActionResult YorumSil(int id)
        {
            var uyeid = Session["uyeid"];
            var yorum = db.yorums.Where(y => y.yorumid == id).SingleOrDefault();
            var makale = db.makales.Where(m => m.makaleid == yorum.makaleid).SingleOrDefault();
            if (yorum.uyeid == Convert.ToInt32(uyeid))
            {
                db.yorums.Remove(yorum);
                db.SaveChanges();
                return RedirectToAction("MakaleDetay", "Home", new { id = makale.makaleid });
            }
            else
            {
                return HttpNotFound();
            }


        }
        public ActionResult BlogAra(string Ara = null)
        {
            var aranan = db.makales.Where(m => m.baslik.Contains(Ara)).ToList();
            return View(aranan.OrderByDescending(m => m.tarih));
        }
    }
}