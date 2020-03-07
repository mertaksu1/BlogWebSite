using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mertaksublogg.Models;
using System.Web.Helpers;
using System.IO;
using System.Web.UI;

namespace mertaksublogg.Controllers
{
    public class UyeController : Controller
    {
        mertaksubloggDB db = new mertaksubloggDB();
        // GET: Uye
        public ActionResult Index(int id)
        {
            var uye = db.uyes.Where(u => u.uyeid == id).SingleOrDefault();
            if (Convert.ToInt32(Session["uyeid"]) != uye.uyeid)
            {
                return HttpNotFound();
            }
            return View(uye);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(uye uye)
        {

            var giris = db.uyes.FirstOrDefault(k => k.email == uye.email && k.sifre == uye.sifre);
            if (giris == null)
            {
                return RedirectToAction("Login", "Uye");
            }

            if (giris.email == uye.email && giris.sifre == uye.sifre)
            {
                Session["uyeid"] = giris.uyeid;
                Session["kullaniciadi"] = giris.kullaniciadi;
                Session["yetkiid"] = giris.yetkiid;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(uye uye, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {

                if (Foto != null && uye.adisoyadi != null && uye.email != null && uye.kullaniciadi != null && uye.sifre != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uye.foto = "/Uploads/UyeFoto/" + newfoto;
                    uye.yetkiid = 2;
                    db.uyes.Add(uye);
                    db.SaveChanges();
                    Session["uyeid"] = uye.uyeid;
                    Session["kullaniciadi"] = uye.kullaniciadi;
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    return HttpNotFound();
                }
            }
            return View(uye);
        }
        public ActionResult Edit(int id)
        {
            var uye = db.uyes.Where(u => u.uyeid == id).SingleOrDefault();
            if (Convert.ToInt32(Session["uyeid"]) != uye.uyeid)
            {
                return HttpNotFound();
            }
            return View(uye);
        }
        [HttpPost]
        public ActionResult Edit(uye uye, int id, HttpPostedFileBase Foto)

        {

            if (ModelState.IsValid)
            {
                var uyess = db.uyes.Where(u => u.uyeid == id).SingleOrDefault();
                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(uye.foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(uyess.foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(150, 150);
                    img.Save("~/Uploads/UyeFoto/" + newfoto);
                    uyess.foto = "/Uploads/UyeFoto/" + newfoto;
                }
                uyess.adisoyadi = uye.adisoyadi;
                uyess.kullaniciadi = uye.kullaniciadi;
                uyess.email = uye.email;
                uyess.sifre = uye.sifre;
                db.SaveChanges();
                Session["kullaniciadi"] = uye.kullaniciadi;
                return RedirectToAction("Index", "Uye", new { id = uyess.uyeid });
            }
            return View();
        }
        public ActionResult HerkeseAcikUyeProfil(int id)//herkese açık üye profil sayfası
        {
            var uye = db.uyes.Where(u => u.uyeid == id).SingleOrDefault();
            return View(uye);
        }
        public ActionResult Delete(int id)
        {
            var uye = db.uyes.Where(m => m.uyeid == id).SingleOrDefault();
            if (uye == null)
            {
                return HttpNotFound();
            }
            return View(uye);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var uye = db.uyes.Where(m => m.uyeid == id).SingleOrDefault();
                if (uye == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(uye.foto)))
                {
                    System.IO.File.Delete(Server.MapPath(uye.foto));
                }
                foreach (var i in uye.yorums.ToList())
                {
                    db.yorums.Remove(i);
                }

                db.uyes.Remove(uye);
                db.SaveChanges();
                Session["uyeid"] = null;
                Session.Abandon();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

    }
}