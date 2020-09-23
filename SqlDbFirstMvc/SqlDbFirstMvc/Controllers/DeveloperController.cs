using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SqlDbFirstMvc.Controllers
{
    public class DeveloperController : Controller
    {
        SqlDbFirstMvcEntities db = new SqlDbFirstMvcEntities();
        // GET: Developer
        public ActionResult Index()
        {
            return View(db.Developers.ToList());
        }


        public ActionResult Details(int Id)
        {
            return View(db.Developers.Where(x => x.id == Id).FirstOrDefault());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Developer developer)
        {
            if (ModelState.IsValid)
            {
                db.Developers.Add(developer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(developer);
        }

        public ActionResult Edit (int Id)
        {
            return View(db.Developers.Where(x => x.id == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit (int Id, Developer developer)
        {
            db.Entry(developer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            return View(db.Developers.Where(x => x.id == Id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            Developer developer = db.Developers.Where(x => x.id == Id).FirstOrDefault();
            db.Developers.Remove(developer);
            db.SaveChanges();
            // return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            return RedirectToAction("Index");
        }

    }
}