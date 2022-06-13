using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Practice8._1.Models;

namespace Practice8._1.Controllers
{
    [Authorize(Users = "admin@mvc.web")]
    public class ProjectEmployeesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        [AllowAnonymous]
        public ActionResult Index()
        {
            var projectEmployees = db.ProjectEmployees.Include(p => p.Employee).Include(p => p.Project);
            var project = db.Projects.ToList();
            var employee = db.Employees.ToList();
            ViewBag.EmployeeID = new SelectList(employee, "EmployeeId", "EmployeeName");
            return View(projectEmployees.ToList());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(string searchName, int EmployeeID = 0)
        {
            var projectEmployees = db.ProjectEmployees.Include(p => p.Employee).Include(p => p.Project).ToList();
            var project = db.Projects.ToList();
            var employee = db.Employees.ToList();
            ViewBag.EmployeeID = new SelectList(employee, "EmployeeId", "EmployeeName");
            if (!string.IsNullOrEmpty(searchName))
            {
                projectEmployees = projectEmployees.Where(x => x.Project.ProjectName.ToLower().Contains(searchName.ToLower())).ToList();
            }
            if (EmployeeID != 0)
            {
                projectEmployees = projectEmployees.Where(x => x.Employee.EmployeeId == EmployeeID).ToList();
            }
            return View(projectEmployees);
        }

        // GET: ProjectEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEmployee projectEmployee = db.ProjectEmployees.Find(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName");
            return View();
        }

        // POST: ProjectEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectEmployeeId,EmployeeId,ProjectId,Task")] ProjectEmployee projectEmployee)
        {
            if (ModelState.IsValid)
            {
                db.ProjectEmployees.Add(projectEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", projectEmployee.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEmployee projectEmployee = db.ProjectEmployees.Find(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", projectEmployee.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // POST: ProjectEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectEmployeeId,EmployeeId,ProjectId,Task")] ProjectEmployee projectEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", projectEmployee.EmployeeId);
            ViewBag.ProjectId = new SelectList(db.Projects, "ProjectId", "ProjectName", projectEmployee.ProjectId);
            return View(projectEmployee);
        }

        // GET: ProjectEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectEmployee projectEmployee = db.ProjectEmployees.Find(id);
            if (projectEmployee == null)
            {
                return HttpNotFound();
            }
            return View(projectEmployee);
        }

        // POST: ProjectEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectEmployee projectEmployee = db.ProjectEmployees.Find(id);
            db.ProjectEmployees.Remove(projectEmployee);
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
