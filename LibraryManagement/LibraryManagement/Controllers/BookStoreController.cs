using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// Add new namespase
using System.Net;
using System.Data.Entity;
using PagedList;

namespace LibraryManagement.Controllers
{
    public class BookStoreController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookStore
        ////////////////////////////[ For Admin Panel]///////////////////////////////////
        public ActionResult Tab()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllBooks());
        }

        ////////////////////////////[ For Sorting, Filtering & Paging]///////////////////////////////////
        public ActionResult AllBooks(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


            if (searchString != null)
            {
                page = 1;
            }
            else { searchString = currentFilter; }
            ViewBag.CurrentFilter = searchString;

            var trainees = from s in db.BookStores
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                trainees = trainees.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    trainees = trainees.OrderByDescending(s => s.Title);
                    break;
                default:
                    trainees = trainees.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(trainees.ToPagedList(pageNumber, pageSize));
        }
        /////////////////////////////[For Admin Panel Book view]///////////////////////////////////
        IEnumerable<BookStore> GetAllBooks()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                return db.BookStores.ToList<BookStore>();
            }
        }
        /////////////////////////////[For Admin panel Add or Edit]////////////////////////////////
        public ActionResult AddorEdit(int id=0)
        {
            BookStore bs = new BookStore();
            return View(bs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult AddorEdit(BookStore bookStore)
        {
            if (bookStore.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(bookStore.ImageUpload.FileName);
                string extension = Path.GetExtension(bookStore.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                bookStore.ImagePath = "~/AppFiles/Images/" + fileName;
                bookStore.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/Images/"), fileName));
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.BookStores.Add(bookStore);
                db.SaveChanges();
            }
            return RedirectToAction ("ViewAll");
        }
        /////////////////////////////[For Admin panel Delete]////////////////////////////////
        [Authorize(Roles = "Officer")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookStore store = db.BookStores.Find(id);
            if (store == null)
            {
                return HttpNotFound();
            }
            return View(store);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            BookStore store = db.BookStores.Find(id);
            db.BookStores.Remove(store);
            db.SaveChanges();
            return RedirectToAction("ViewAll");
        }
        /////////////////////////////[For Admin panel Details]////////////////////////////////
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            BookStore employee = db.BookStores.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        /////////////////////////////[For All Books Details]////////////////////////////////
        public ActionResult DetailsforAllBooks(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            BookStore employee = db.BookStores.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
    }
}