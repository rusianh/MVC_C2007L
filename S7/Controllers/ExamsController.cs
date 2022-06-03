using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using S7.DataConnect;
using S7.Models;
using PagedList;

namespace S7.Controllers
{
    public class ExamsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Exams
        public ViewResult Index(string sortOrder, string searchString, string currentFilter, int? page, [Bind(Include = "StudentId,SubjectId")] Exam exam,string slr)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.SubjectNameSortParm = sortOrder == "SubjectName" ? "subjectname_desc" : "SubjectName";
            ViewBag.ScoreSort = sortOrder == "Score" ? "score_desc" : "Score";
            
            SelectList ddlStu = new SelectList(db.Students, "StudentId", "Name", exam.StudentId);
            SelectList ddlSub = new SelectList(db.Subjects, "SubjectId", "SubjectName", exam.SubjectId);

            ViewBag.SubjectId = ddlSub;
            ViewBag.StudentId= ddlStu;
            var exams = db.Exams.Include(e => e.Student).Include(e => e.Subject);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                exams = exams.Where(s => s.Student.Name.ToLower().Replace(" ", String.Empty).Contains(searchString.ToLower().Replace(" ", String.Empty)) || s.Subject.SubjectName.ToLower().Replace(" ", String.Empty).Contains(searchString.ToLower().Replace(" ", String.Empty)));
            }
            else if ((!String.IsNullOrEmpty(searchString)) || (!ddlStu.Items.Equals("All") || !ddlSub.Items.Equals("All")))
            {
                exams = exams.Where(s => s.SubjectId == exam.SubjectId || s.StudentId == exam.StudentId);
            }
            //else if (String.IsNullOrEmpty(searchString) && !ddlStu.Items.Equals("All") && ddlSub.Items.Equals("All"))
            //{
            //    exams = exams.Where(s =>  s.StudentId == exam.StudentId);
            //}
            //else if (String.IsNullOrEmpty(searchString) && !ddlSub.Items.Equals("All") && ddlStu.Items.Equals("All"))
            //{
            //    exams = exams.Where(s => s.SubjectId == exam.SubjectId);
            //}
            //else if (String.IsNullOrEmpty(searchString) && (!ddlStu.Items.Equals("All") && !ddlSub.Items.Equals("All")))
            //{
            //    exams = exams.Where(s => s.StudentId == exam.StudentId && s.SubjectId == exam.SubjectId);
            //}
            else if ((String.IsNullOrEmpty(searchString)) && (ddlStu.Items.Equals("All") && ddlSub.Items.Equals("All")))
            {
                return View(exams.ToList());
            }



            switch (sortOrder)
            {
                case "name_desc":
                    exams = exams.OrderByDescending(s => s.Student.Name);
                    break;
                case "SubjectName":
                    exams = exams.OrderBy(s => s.Student.Name);
                    break;
                case "subjectname_desc":
                    exams = exams.OrderByDescending(s => s.Subject.SubjectName);
                    break;
                case "Score":
                    exams = exams.OrderBy(s => s.Score);
                    break;
                case "score_desc":
                    exams = exams.OrderByDescending(s => s.Score);
                    break;
                default:
                    exams = exams.OrderBy(s => s.Student.Name);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(exams.ToPagedList(pageNumber, pageSize));

        }

        // GET: Exams/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exams.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // GET: Exams/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name");
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Score,StudentId,SubjectId")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Exams.Add(exam);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", exam.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", exam.SubjectId);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exams.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", exam.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", exam.SubjectId);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Score,StudentId,SubjectId")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "Name", exam.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "SubjectId", "SubjectName", exam.SubjectId);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam exam = await db.Exams.FindAsync(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Exam exam = await db.Exams.FindAsync(id);
            db.Exams.Remove(exam);
            await db.SaveChangesAsync();
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
