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
        public ActionResult Index(string searchString)
        {
            ViewBag.Sum = db.StudentFees.Sum(item => item.EnrollmentFee);
            ViewBag.EnrollmentCount = db.StudentFees.Count();

            var students = from item in db.StudentFees
                           select item;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = from item in students
                           where item.LastName.Contains(searchString) ||
                                 item.FirstName.Contains(searchString)
                           select item;
            }

            return View(students.ToList());
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

        // method to return student fee based on a number of conditons
        private int returnStudentFees(string name1, string name2)
        {
            int fee = 200;
            if (name1 == null)
            {
                name1 = "";
            }
            if (name2 == null)
            {
                name2 = "";
            }
            string firstname = name1.ToLower();
            string lastname = name2.ToLower();

            if ((firstname.Contains("malfoy")) || (lastname.Contains("malfoy")))
            {
                return (-1);
            }
            if ((firstname.Contains("tom")) || (lastname.Contains("tom")))
            {
                return (-1);
            }
            if ((firstname.Contains("riddle")) || (lastname.Contains("riddle")))
            {
                return (-1);
            }
            if ((firstname.Contains("voldemort")) || (lastname.Contains("voldemort")))
            {
                return (-1);
            }

            if (firstname.First() == lastname.First())
            {
                fee = Convert.ToInt32(fee * .90);
            }

            if ((firstname.Contains("longbottom") || lastname.Contains("longbottom")) & (db.StudentFees.Count() <= 10))
            {
                fee = 0;
            }

            if ((firstname.Contains("potter")) || (lastname.Contains("potter")))
            {
                fee = Convert.ToInt32(fee * .50);
            }

            return fee;
        }

        // POST: StudentFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName")] StudentFee studentFee)
        {
            studentFee.EnrollmentFee = returnStudentFees(studentFee.FirstName, studentFee.LastName);

            if (studentFee.EnrollmentFee == -1)
            {
                ViewBag.Result = "Cannot be enrolled. He who must not be named !!";
                return View();
            }

            if (ModelState.IsValid)
            {
                db.StudentFees.Add(studentFee);
                ViewBag.EnrollmentNumber = db.StudentFees.Count();
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
