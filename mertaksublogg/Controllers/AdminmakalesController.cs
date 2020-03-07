using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mertaksublogg.Models;
using System.Net;
using System.Web.Helpers;
using System.IO;

namespace mertaksublogg.Controllers
{
    public class AdminmakalesController : Controller
    {
        mertaksubloggDB db = new mertaksubloggDB();
        // GET: Adminmakales
        public ActionResult Index()
        {
            var makales = db.makales.OrderByDescending(m => m.makaleid).ToList();
            return View(makales);
        }

        // GET: Adminmakales/Details/5
        public ActionResult Details(int id)
        {
            var makale = db.makales.Where(m => m.makaleid == id).SingleOrDefault();
            return View(makale);
        }

        // GET: Adminmakales/Create
        public ActionResult Create()
        {

            ViewBag.kategoriid = new SelectList(db.kategoris, "kategoriid", "kategoriadi");
            return View();
        }

        // POST: Adminmakales/Create
        [HttpPost]
        public ActionResult Create(makale makale, HttpPostedFileBase Foto, HttpPostedFileBase Foto2)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    WebImage img2 = new WebImage(Foto2.InputStream);
                    FileInfo fotoinfo2 = new FileInfo(Foto2.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    string newfoto2 = Guid.NewGuid().ToString() + fotoinfo2.Extension;

                    img.Resize(670, 320);
                    img2.Resize(670, 320);
                    img.Save("~/Uploads/MakaleFoto/" + newfoto);
                    img2.Save("~/Uploads/MakaleFoto/" + newfoto2);
                    makale.buyukfoto = "/Uploads/MakaleFoto/" + newfoto;
                    makale.kucukfoto = "/Uploads/MakaleFoto/" + newfoto2;

                }
                makale.okunma = 1;
                makale.uyeid = Convert.ToInt32(Session["uyeid"]);
                db.makales.Add(makale);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(makale);
        }

        // GET: Adminmakales/Edit/5
        public ActionResult Edit(int id)
        {
            var makale = db.makales.Where(m => m.makaleid == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }
            ViewBag.kategoriid = new SelectList(db.kategoris, "kategoriid", "kategoriadi", makale.kategoriid);
            return View(makale);
        }

        // POST: Adminmakales/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFileBase Foto, makale makale, HttpPostedFileBase Foto2)
        {
            try
            {
                var makales = db.makales.Where(m => m.makaleid == id).SingleOrDefault();
                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(makales.buyukfoto)))
                    {
                        System.IO.File.Delete(Server.MapPath(makales.buyukfoto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);
                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/MakaleFoto/" + newfoto);
                    makales.buyukfoto = "/Uploads/MakaleFoto/" + newfoto;
                }
                if (Foto2 != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(makales.kucukfoto)))
                    {
                        System.IO.File.Delete(Server.MapPath(makales.kucukfoto));
                    }
                    WebImage img2 = new WebImage(Foto2.InputStream);
                    FileInfo fotoinfo2 = new FileInfo(Foto2.FileName);
                    string newfoto2 = Guid.NewGuid().ToString() + fotoinfo2.Extension;
                    img2.Resize(800, 350);
                    img2.Save("~/Uploads/MakaleFoto/" + newfoto2);
                    makales.kucukfoto = "/Uploads/MakaleFoto/" + newfoto2;
                }
                
                makales.baslik = makale.baslik;
                makales.ozet = makale.ozet;
                makales.icerik = makale.icerik;
                makales.tarih = makales.tarih;
                makales.kategoriid = makale.kategoriid;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.kategoriid = new SelectList(db.kategoris, "kategoriid", "kategoriadi", makale.kategoriid);
                return View(makale);
            }
        }

        // GET: Adminmakales/Delete/5
        public ActionResult Delete(int id)
        {
            var makale = db.makales.Where(m => m.makaleid == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }
            return View(makale);
        }

        // POST: Adminmakales/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var makale = db.makales.Where(m => m.makaleid == id).SingleOrDefault();
                if (makale == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(makale.buyukfoto)))
                {
                    System.IO.File.Delete(Server.MapPath(makale.buyukfoto));
                }
                if (System.IO.File.Exists(Server.MapPath(makale.kucukfoto)))
                {
                    System.IO.File.Delete(Server.MapPath(makale.kucukfoto));
                }
                foreach (var i in makale.yorums.ToList())
                {
                    db.yorums.Remove(i);
                }

                db.makales.Remove(makale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
