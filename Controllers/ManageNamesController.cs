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
using System.Data.Entity.Infrastructure;

namespace NamesRecommender.Controllers
{
    public class ManageNamesController : Controller
    {
        private NamesContext db = new NamesContext();
        ILog logger = LogManager.GetLogger(typeof(SearchNamesController));

        // GET: ManageNames
        public ActionResult Index()
        {
            var names = db.Names.Include(n => n.category).Include(n => n.gender).Include(n => n.length).Include(n => n.origin).Include(n => n.type);
            return View(names.ToList());
        }

        // GET: ManageNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                logger.Error("Admin:Details--Null id");
                throw new HttpException(404, "Bad Request");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameDetail nameDetail = db.Names.Find(id);
            if (nameDetail == null)
            {
                logger.Error("Admin:Details--id not found");
                throw new Exception("Name Id not found!");
                //return HttpNotFound();
            }
            return View(nameDetail);
        }

        // GET: ManageNames/Create
        public ActionResult Create()
        {
            ViewBag.NameCategoryId = new SelectList(db.Categories, "NameCategoryId", "Category");
            ViewBag.NameGenderId = new SelectList(db.Genders, "NameGenderId", "Gender");
            ViewBag.NameLengthId = new SelectList(db.Lengths, "NameLengthId", "Length");
            ViewBag.NameOriginId = new SelectList(db.Origins, "NameOriginId", "Origin");
            ViewBag.NameTypeId = new SelectList(db.Types, "NameTypeId", "Type");
            return View();
        }

        // POST: ManageNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NameDetailId,NameText,Meaning,NamesInfo,stamp,NameGenderId,NameCategoryId,NameTypeId,NameOriginId,NameLengthId")] NameDetail nameDetail)
        {
            if (ModelState.IsValid)
            {
                db.Names.Add(nameDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NameCategoryId = new SelectList(db.Categories, "NameCategoryId", "Category", nameDetail.NameCategoryId);
            ViewBag.NameGenderId = new SelectList(db.Genders, "NameGenderId", "Gender", nameDetail.NameGenderId);
            ViewBag.NameLengthId = new SelectList(db.Lengths, "NameLengthId", "Length", nameDetail.NameLengthId);
            ViewBag.NameOriginId = new SelectList(db.Origins, "NameOriginId", "Origin", nameDetail.NameOriginId);
            ViewBag.NameTypeId = new SelectList(db.Types, "NameTypeId", "Type", nameDetail.NameTypeId);
            return View(nameDetail);
        }

        // GET: ManageNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                logger.Error("Admin:Edit--Null Id");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameDetail nameDetail = db.Names.Find(id);
            if (nameDetail == null)
            {
                logger.Error("Admin:Edit--id not found");
                return HttpNotFound();
            }
            ViewBag.NameCategoryId = new SelectList(db.Categories, "NameCategoryId", "Category", nameDetail.NameCategoryId);
            ViewBag.NameGenderId = new SelectList(db.Genders, "NameGenderId", "Gender", nameDetail.NameGenderId);
            ViewBag.NameLengthId = new SelectList(db.Lengths, "NameLengthId", "Length", nameDetail.NameLengthId);
            ViewBag.NameOriginId = new SelectList(db.Origins, "NameOriginId", "Origin", nameDetail.NameOriginId);
            ViewBag.NameTypeId = new SelectList(db.Types, "NameTypeId", "Type", nameDetail.NameTypeId);
            return View(nameDetail);
        }

        // POST: ManageNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NameDetailId,NameText,Meaning,NamesInfo,stamp,NameGenderId,NameCategoryId,NameTypeId,NameOriginId,NameLengthId")] NameDetail nameDetail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(nameDetail).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            //ex holds what data is currently in database
            //and what data is the user trying to enter
            catch (DbUpdateConcurrencyException ex)
            {
                //get the client's value
                //and
                //get the database's value

                var entry = ex.Entries.Single();
                var clientValues = (NameDetail)entry.Entity;
                var databaseEntry = entry.GetDatabaseValues();

                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to save changes. The name details was deleted by another user.");
                }
                else
                {
                    var databaseValues = (NameDetail)databaseEntry.ToObject();

                    if (databaseValues.NameText != clientValues.NameText)
                    {
                        ModelState.AddModelError("NameText", "Current value: " + databaseValues.NameText);
                    }

                    if (databaseValues.Meaning != clientValues.Meaning)
                    {
                        ModelState.AddModelError("Meaning", "Current value: " + databaseValues.Meaning);
                    }

                    if (databaseValues.NamesInfo != clientValues.NamesInfo)
                    {
                        ModelState.AddModelError("NamesInfo", "Current value: " + databaseValues.NamesInfo);
                    }

                    if (databaseValues.NameCategoryId != clientValues.NameCategoryId)
                    {
                        ModelState.AddModelError("NameCategoryId", "Current value: " + db.Categories.Single(p => p.NameCategoryId == databaseValues.NameCategoryId).Category);
                    }

                    if (databaseValues.NameGenderId != clientValues.NameGenderId)
                    {
                        ModelState.AddModelError("NameGenderId", "Current value: " + db.Genders.Single(p => p.NameGenderId == databaseValues.NameGenderId).Gender);
                    }

                    if (databaseValues.NameOriginId != clientValues.NameOriginId)
                    {
                        ModelState.AddModelError("NameOriginId", "Current value: " + db.Origins.Single(p => p.NameOriginId == databaseValues.NameOriginId).Origin);
                    }

                    if (databaseValues.NameTypeId != clientValues.NameTypeId)
                    {
                        ModelState.AddModelError("NameTypeId", "Current value: " + db.Types.Single(p => p.NameTypeId == databaseValues.NameTypeId).Type);
                    }
                    if (databaseValues.NameLengthId != clientValues.NameLengthId)
                    {
                        ModelState.AddModelError("NameLengthId", "Current value: " + db.Lengths.Single(p => p.NameLengthId == databaseValues.NameLengthId).Length);
                    }
                    ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                        + "was modified by another user after you got the original value. The "
                        + "edit operation was canceled and the current values in the database "
                        + "have been displayed. If you still want to edit this record, click "
                        + "the Save button again. Otherwise click the Back to List hyperlink.");

                    nameDetail.stamp = databaseValues.stamp;
                }
            }

            ViewBag.NameCategoryId = new SelectList(db.Categories, "NameCategoryId", "Category", nameDetail.NameCategoryId);
            ViewBag.NameGenderId = new SelectList(db.Genders, "NameGenderId", "Gender", nameDetail.NameGenderId);
            ViewBag.NameOriginId = new SelectList(db.Origins, "NameOriginId", "Origin", nameDetail.NameOriginId);
            ViewBag.NameTypeId = new SelectList(db.Types, "NameTypeId", "Type", nameDetail.NameTypeId);
            ViewBag.NameLengthId = new SelectList(db.Lengths, "NameLengthId", "Length", nameDetail.NameLengthId);
            return View(nameDetail);
        }

        // GET: ManageNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                logger.Error("Admin:Delete--Null Id");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NameDetail nameDetail = db.Names.Find(id);
            if (nameDetail == null)
            {
                logger.Error("Admin:Delete--No Names Id");
                return HttpNotFound();
            }
            return View(nameDetail);
        }

        // POST: ManageNames/Delete/5
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
