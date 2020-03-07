using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mertaksublogg.Models;

namespace mertaksublogg.Controllers
{
    public class AdminyorumsController : Controller
    {
        private mertaksubloggDB db = new mertaksubloggDB();

        // GET: Adminyorums
        public ActionResult Index()
        {
            var yorums = db.yorums.Include(y => y.makale).Include(y => y.uye);
            return View(yorums.ToList());
        }

        // GET: Adminyorums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // GET: Adminyorums/Create
        public ActionResult Create()
        {
            ViewBag.makaleid = new SelectList(db.makales, "makaleid", "baslik");
            ViewBag.uyeid = new SelectList(db.uyes, "uyeid", "kullaniciadi");
            return View();
        }

        // POST: Adminyorums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "yorumid,icerik,uyeid,makaleid,tarih")] yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.yorums.Add(yorum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.makaleid = new SelectList(db.makales, "makaleid", "baslik", yorum.makaleid);
            ViewBag.uyeid = new SelectList(db.uyes, "uyeid", "kullaniciadi", yorum.uyeid);
            return View(yorum);
        }

        // GET: Adminyorums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            ViewBag.makaleid = new SelectList(db.makales, "makaleid", "baslik", yorum.makaleid);
            ViewBag.uyeid = new SelectList(db.uyes, "uyeid", "kullaniciadi", yorum.uyeid);
            return View(yorum);
        }

        // POST: Adminyorums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "yorumid,icerik,uyeid,makaleid,tarih")] yorum yorum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yorum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.makaleid = new SelectList(db.makales, "makaleid", "baslik", yorum.makaleid);
            ViewBag.uyeid = new SelectList(db.uyes, "uyeid", "kullaniciadi", yorum.uyeid);
            return View(yorum);
        }

        // GET: Adminyorums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yorum yorum = db.yorums.Find(id);
            if (yorum == null)
            {
                return HttpNotFound();
            }
            return View(yorum);
        }

        // POST: Adminyorums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            yorum yorum = db.yorums.Find(id);
            db.yorums.Remove(yorum);
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
