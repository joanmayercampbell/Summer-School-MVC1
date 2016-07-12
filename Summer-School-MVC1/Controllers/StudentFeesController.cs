using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Summer_School_MVC1.Models;

namespace Summer_School_MVC1.Controllers
{
    public class StudentFeesController : Controller
    {
        private SummerSchoolEntities db = new SummerSchoolEntities();

        // GET: StudentFees
        public ActionResult Index()
        {
            return View(db.StudentFees.ToList());
        }

        // GET: StudentFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFee studentFee = db.StudentFees.Find(id);
            if (studentFee == null)
            {
                return HttpNotFound();
            }
            return View(studentFee);
        }

        // GET: StudentFees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,EnrollmentFee")] StudentFee studentFee)
        {
            if (ModelState.IsValid)
            {
                db.StudentFees.Add(studentFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentFee);
        }

        // GET: StudentFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFee studentFee = db.StudentFees.Find(id);
            if (studentFee == null)
            {
                return HttpNotFound();
            }
            return View(studentFee);
        }

        // POST: StudentFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,EnrollmentFee")] StudentFee studentFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentFee);
        }

        // GET: StudentFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFee studentFee = db.StudentFees.Find(id);
            if (studentFee == null)
            {
                return HttpNotFound();
            }
            return View(studentFee);
        }

        // POST: StudentFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentFee studentFee = db.StudentFees.Find(id);
            db.StudentFees.Remove(studentFee);
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
