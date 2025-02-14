﻿using System;
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
    public class AdminyetkisController : Controller
    {
        private mertaksubloggDB db = new mertaksubloggDB();

        // GET: Adminyetkis
        public ActionResult Index()
        {
            return View(db.yetkis.ToList());
        }

        // GET: Adminyetkis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yetki yetki = db.yetkis.Find(id);
            if (yetki == null)
            {
                return HttpNotFound();
            }
            return View(yetki);
        }

        // GET: Adminyetkis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Adminyetkis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "yetkiid,yetkiadi")] yetki yetki)
        {
            if (ModelState.IsValid)
            {
                db.yetkis.Add(yetki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(yetki);
        }

        // GET: Adminyetkis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yetki yetki = db.yetkis.Find(id);
            if (yetki == null)
            {
                return HttpNotFound();
            }
            return View(yetki);
        }

        // POST: Adminyetkis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "yetkiid,yetkiadi")] yetki yetki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yetki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(yetki);
        }

        // GET: Adminyetkis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yetki yetki = db.yetkis.Find(id);
            if (yetki == null)
            {
                return HttpNotFound();
            }
            return View(yetki);
        }

        // POST: Adminyetkis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            yetki yetki = db.yetkis.Find(id);
            db.yetkis.Remove(yetki);
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
