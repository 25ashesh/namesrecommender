using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NamesRecommender.Models;
using log4net;

namespace NamesRecommender.Controllers
{
    public class SearchNamesController : Controller
    {
        private NamesContext db = new NamesContext();
        ILog logger = LogManager.GetLogger(typeof(SearchNamesController));

        //*SEARCHBOX*//
        public ActionResult Search(int Gender, int Origin, int Category, int Type, string starting, string rhyming)
        {
            //var results = db.Names.Where(
            //    p => p.gender.Equals(db.Genders.Where(g=>g.NameGenderId.Equals(Gender)))
            //    && p.origin.Equals(db.Origins.Where(o => o.NameOriginId.Equals(Origin)))
            //    && p.category.Equals(db.Categories.Where(c=>c.NameCategoryId.Equals(Category)))
            //    && p.type.Equals(db.Types.Where(t=>t.NameTypeId.Equals(Type))));
            //var res = (from s in db.Names
            //           where s.gender.NameGenderId.Equals(Gender) && s.origin.NameOriginId.Equals(Origin) && s.category.NameCategoryId.Equals(Category) && s.type.NameTypeId.Equals(Type)
            //           select new { s.NameText}).ToList();


            var res = db.Names.Where(r =>
                r.gender.NameGenderId.Equals(Gender)
                &&
                r.origin.NameOriginId.Equals(Origin)
                &&
                r.category.NameCategoryId.Equals(Category)
                &&
                r.type.NameTypeId.Equals(Type)
                &&
                r.NameText.StartsWith(starting)
                &&
                r.NameText.Contains(rhyming)
                );

            return View("Index", res.ToList());
            //return View();
        }

        // GET: SearchNames
        public ActionResult Index()
        {
            var names = db.Names.Include(n => n.category).Include(n => n.gender).Include(n => n.origin).Include(n => n.type);
            return View(names.ToList());
        }

        // GET: SearchNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                logger.Error("User:Details--Null id");
                throw new HttpException(404, "Bad Request");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameDetail nameDetail = db.Names.Find(id);
            if (nameDetail == null)
            {
                logger.Error("User:Details--id not found");
                throw new Exception("Name Id not found!");
                //return HttpNotFound();
            }
            //DEBUG
            //logger.Debug("User:Details--return details");
            return View(nameDetail);
        }

        // GET: SearchNames/Create
        public ActionResult Create()
        {
            ViewBag.NameCategoryId = new SelectList(db.Categories, "NameCategoryId", "Category");
            ViewBag.NameGenderId = new SelectList(db.Genders, "NameGenderId", "Gender");
            ViewBag.NameOriginId = new SelectList(db.Origins, "NameOriginId", "Origin");
            ViewBag.NameTypeId = new SelectList(db.Types, "NameTypeId", "Type");
            return View();
        }

        // POST: SearchNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NameDetailId,NameText,Meaning,NamesInfo,NameGenderId,NameCategoryId,NameTypeId,NameOriginId")] NameDetail nameDetail)
        {
            if (ModelState.IsValid)
            {
                db.Names.Add(nameDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NameCategoryId = new SelectList(db.Categories, "NameCategoryId", "Category", nameDetail.NameCategoryId);
            ViewBag.NameGenderId = new SelectList(db.Genders, "NameGenderId", "Gender", nameDetail.NameGenderId);
            ViewBag.NameOriginId = new SelectList(db.Origins, "NameOriginId", "Origin", nameDetail.NameOriginId);
            ViewBag.NameTypeId = new SelectList(db.Types, "NameTypeId", "Type", nameDetail.NameTypeId);
            return View(nameDetail);
        }

        // GET: SearchNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                logger.Error("User:Edit--Null id");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameDetail nameDetail = db.Names.Find(id);
            if (nameDetail == null)
            {
                logger.Error("User:Edit--id not found");
                return HttpNotFound();
            }
            ViewBag.NameCategoryId = new SelectList(db.Categories, "NameCategoryId", "Category", nameDetail.NameCategoryId);
            ViewBag.NameGenderId = new SelectList(db.Genders, "NameGenderId", "Gender", nameDetail.NameGenderId);
            ViewBag.NameOriginId = new SelectList(db.Origins, "NameOriginId", "Origin", nameDetail.NameOriginId);
            ViewBag.NameTypeId = new SelectList(db.Types, "NameTypeId", "Type", nameDetail.NameTypeId);
            return View(nameDetail);
        }

        // POST: SearchNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NameDetailId,NameText,Meaning,NamesInfo,NameGenderId,NameCategoryId,NameTypeId,NameOriginId")] NameDetail nameDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nameDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NameCategoryId = new SelectList(db.Categories, "NameCategoryId", "Category", nameDetail.NameCategoryId);
            ViewBag.NameGenderId = new SelectList(db.Genders, "NameGenderId", "Gender", nameDetail.NameGenderId);
            ViewBag.NameOriginId = new SelectList(db.Origins, "NameOriginId", "Origin", nameDetail.NameOriginId);
            ViewBag.NameTypeId = new SelectList(db.Types, "NameTypeId", "Type", nameDetail.NameTypeId);
            return View(nameDetail);
        }

        // GET: SearchNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                logger.Error("User:Delete--Null Id");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameDetail nameDetail = db.Names.Find(id);
            if (nameDetail == null)
            {
                logger.Error("User:Delete--No Names id");
                return HttpNotFound();
            }
            return View(nameDetail);
        }

        // POST: SearchNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NameDetail nameDetail = db.Names.Find(id);
            db.Names.Remove(nameDetail);
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
