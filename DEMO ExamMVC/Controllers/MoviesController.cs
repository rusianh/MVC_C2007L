using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DEMO_ExamMVC.Models;
using PagedList;

namespace DEMO_ExamMVC.Controllers
{
    [Authorize(Users="admin@mvc.com")]
    public class MoviesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Movies
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, int page =1, int pageSize =3)
        {
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";
            ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";

            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";

            //ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            //List Movies
            var movies = db.Movies.Include(m => m.Genre);
            //Switch sortOrder
            switch (sortOrder) {
                case "title_desc":
                    movies = movies.OrderByDescending(s => s.Title);
                    break;
                case "Title":
                    movies = movies.OrderBy(s => s.Title);
                    break;
                case "name_desc":
                    movies = movies.OrderByDescending(s => s.Genre.GenreName);
                    break;
                //case "name_asc":
                //    movies = movies.OrderBy(s => s.Genre.GenreName);
                //    break;
                //case "date_desc":
                //    movies = movies.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    movies = movies.OrderBy(s => s.Genre.GenreName);
                    break;
            }
            //switch order end
                    return View(movies.ToList().ToPagedList(page,pageSize));
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName");
            return View();
        }

        //[HttpPost]
        //public ActionResult Create()
        //{
        //    ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName");
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

        //    return View();
        //}

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieId,Title,ReleaseDate,RunningTime,GenreId,BoxOffice")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", movie.GenreId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,Title,ReleaseDate,RunningTime,GenreId,BoxOffice")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", movie.GenreId);
            

            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
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
