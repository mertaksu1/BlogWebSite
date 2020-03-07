using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mertaksublogg.Models;
using System.Web.Helpers;
using System.IO;

namespace mertaksublogg.Controllers
{
    public class AdminuyesController : Controller
    {
        private mertaksubloggDB db = new mertaksubloggDB();

        // GET: Adminuyes
        public ActionResult Index()
        {
            var uyes = db.uyes.Include(u => u.yetki);
            return View(uyes.ToList());
        }

        // GET: Adminuyes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uye uye = db.uyes.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // GET: Adminuyes/Create
        public ActionResult Create()
        {
            ViewBag.yetkiid = new SelectList(db.yetkis, "yetkiid", "yetkiadi");
            return View();
        }

        // POST: Adminuyes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(uye uye, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(670, 320);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uye.foto = "/Uploads/UyeFoto/" + newfoto;

                }
                db.uyes.Add(uye);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uye);
        }

        // GET: Adminuyes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uye uye = db.uyes.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            ViewBag.yetkiid = new SelectList(db.yetkis, "yetkiid", "yetkiadi", uye.yetkiid);
            return View(uye);
        }

        // POST: Adminuyes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HttpPostedFileBase Foto, uye uye)
        {
                var uyes = db.uyes.Where(m => m.uyeid == id).SingleOrDefault();
                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(uyes.foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(uyes.foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uyes.foto = "/Uploads/UyeFoto/" + newfoto;
                    uyes.kullaniciadi = uye.kullaniciadi;
                    uyes.email = uye.email;
                    uyes.sifre = uye.sifre;
                    uyes.adisoyadi = uye.adisoyadi;
                    uyes.yetkiid = uye.yetkiid;
                    db.SaveChanges();
                }
                else
                {
                    uyes.kullaniciadi = uye.kullaniciadi;
                    uyes.email = uye.email;
                    uyes.sifre = uye.sifre;
                    uyes.adisoyadi = uye.adisoyadi;
                    uyes.yetkiid = uye.yetkiid;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
          
        }

        // GET: Adminuyes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uye uye = db.uyes.Find(id);
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }

        // POST: Adminuyes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            uye uye = db.uyes.Find(id);
            db.uyes.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
