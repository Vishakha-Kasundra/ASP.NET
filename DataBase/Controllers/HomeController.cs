using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase.Models;

namespace DataBase.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MyDbEntities db = new MyDbEntities();
            List<Student> std=db.Students.ToList();
            return View(std);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Registration(int? id)
        {
            MyDbEntities db = new MyDbEntities();
            Student s = new Student();
            db.SaveChanges();
            if(id.HasValue)
            {
                s = db.Students.Find(id);
            }
            return View(s);
        }

        [HttpPost]
        public ActionResult Registration(Student s)
        {
            MyDbEntities db = new MyDbEntities();
            if(s.Id==0)
            {
                db.Students.Add(s);

            }
            else
            {
                Student s1 = db.Students.Find(s.Id);
                s1.Name = s.Name;
                s1.City = s.City;
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            MyDbEntities db=new MyDbEntities();
            Student stud = db.Students.Find(id);
            db.Students.Remove(stud);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}