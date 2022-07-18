using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EAP_C2007L_NguyenDucHuy.Models;
using PagedList;

namespace EAP_C2007L_NguyenDucHuy.Controllers
{
    [Authorize(Users ="admin@mvc.com")]
    public class AlbumsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Albums
        [AllowAnonymous]
        public  ActionResult Index(string SearchString, string sortOrder, int? page, int? PageSize,string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.GenreParm = string.IsNullOrEmpty(sortOrder) ? "genre_desc" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.ReleaseDateParm = sortOrder == "ReleaseDate" ? "date_desc" : "ReleaseDate";
            ViewBag.ArtistParm = sortOrder == "Artist" ? "artist_desc" : "Artist";
            ViewBag.PriceParm = sortOrder == "Price" ? "price_desc" : "Price";

            var albums = db.Albums.Include(a => a.Genres);

            if(SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            if (!string.IsNullOrEmpty(SearchString))
            {
                albums = albums.Where(x => x.Title.ToLower().Replace(" ", string.Empty).Contains(SearchString.ToLower().Replace(" ", string.Empty)));
            }
        

            switch (sortOrder)
            {
                case "genre_desc":
                    albums = albums.OrderByDescending(x => x.Genres.GenreName);
                    break;
                case "Title":
                    albums = albums.OrderBy(x => x.Title);
                    break;
                case "title_desc":
                    albums = albums.OrderByDescending(x => x.Title);
                    break;
                case "RealeaseDate":
                    albums = albums.OrderBy(x => x.ReleaseDate);
                    break;
                case "date_desc":
                    albums = albums.OrderByDescending(x => x.ReleaseDate);
                    break;
                case "Artist":
                    albums = albums.OrderBy(x => x.Artist);
                    break;
                case "artist_desc":
                    albums = albums.OrderByDescending(x => x.Artist);
                    break;
                case "Price":
                    albums = albums.OrderBy(x => x.Price);
                    break;
                case "price_desc":
                    albums = albums.OrderByDescending(x => x.Price);
                    break;
                default:
                    albums = albums.OrderBy(x => x.Genres.GenreName);
                    break;
            }
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem(){Value="3",Text="3"},
                new SelectListItem(){Value="5",Text="5"},
                new SelectListItem(){Value="10",Text="10"},
                new SelectListItem(){Value="20",Text="20"},
            };
            int pageSize = (PageSize ?? 5);
            ViewBag.Pgsize = pageSize;
            ViewBag.Count = albums.Count();
            int pageNumber = (page ?? 1);
            return View(albums.ToPagedList(pageNumber,pageSize));
        }
        
        // GET: Albums/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AlbumId,Title,ReleaseDate,Artist,Price,GenreId")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", album.GenreId);
            return View(album);
        }

        // GET: Albums/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", album.GenreId);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AlbumId,Title,ReleaseDate,Artist,Price,GenreId")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", album.GenreId);
            return View(album);
        }

        // GET: Albums/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Album album = await db.Albums.FindAsync(id);
            db.Albums.Remove(album);
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
