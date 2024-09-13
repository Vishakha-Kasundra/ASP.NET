/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database.Models;

namespace Database.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string searchTerm)
        {
            MydbEntities db = new MydbEntities();
            var std = db.Students.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                std = std.Where(s => s.Name.Contains(searchTerm) || s.City.Contains(searchTerm));
            }
            return View(std.ToList());
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
            
            if(id!=0)
            {
                MydbEntities db = new MydbEntities();
                Student s = db.Students.Find(id);
                return View(s);
            }
            else {
                return View(); 
            }
           
        }
        [HttpPost]
        public ActionResult Registration(Student s)
        {
            MydbEntities db = new MydbEntities();

            *//*if(s.Id==0)
            {
                db.Students.Add(s);
            }
            else
            {
                Student ss = db.Students.Find(s.Id);
                ss.Name = s.Name;
                ss.City = s.City;
            }*//*

            if (s.Id != 0)
            {
                *//*Update code*//*
                Student ss = db.Students.Find(s.Id);
                ss.Name = s.Name;
                ss.City = s.City;
               
            }
            else
            {
               *//* Add code*//*
                db.Students.Add(s);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            MydbEntities db = new MydbEntities();
            Student stud = db.Students.Find(id);
            db.Students.Remove(stud);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult DeleteSelected(int[] selectedIds)
        {
            MydbEntities db = new MydbEntities();
            if (selectedIds != null)
            {
                foreach (int id in selectedIds)
                {
                    Student stud = db.Students.Find(id);
                    if (stud != null)
                    {
                        db.Students.Remove(stud);
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}*/



using System.Linq;
using System.Web.Mvc;
using Database.Models;

namespace Database.Controllers
{
    public class HomeController : Controller
    {
        // Method to list students with optional search

        /* public ActionResult Index(string searchTerm,string searchBy)
         {
             MydbEntities std = new MydbEntities();

             if (searchBy == "Name")
                 {
                    var name = std.Students.Where(s => s.Name.Contains(searchTerm));
                    return View(name);
                 }

                 else if(searchBy == "City")
                 {
                    var city = std.Students.Where(s => s.City.Contains(searchTerm));
                    return View(city);
                 }                   
         }*/

        public ActionResult Index(string searchTerm, string searchBy)
        {
            MydbEntities std = new MydbEntities();

            IQueryable<Student> students = std.Students;

            if (searchBy == "Name")
            {
                students = students.Where(s => s.Name.Contains(searchTerm));
            }
            else if (searchBy == "City")
            {
                students = students.Where(s => s.City.Contains(searchTerm));
            }

            // Handle the case where searchBy is neither "Name" nor "City"
            // or where searchTerm is null/empty
            // Here, we'll assume that we want to return an empty result set if no valid searchBy value is provided
            return View(students.ToList());
        }


        // About page
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        // Contact page
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        // Registration page (GET)
        public ActionResult Registration(int? id)
        {
            using (var db = new MydbEntities())
            {
                if (id.HasValue)
                {
                    var student = db.Students.Find(id.Value);
                    return View(student);
                }
                return View();
            }
        }

        // Registration page (POST)
        [HttpPost]
        public ActionResult Registration(Student student)
        {
            using (var db = new MydbEntities())
            {
                if (student.Id > 0)
                {
                    var existingStudent = db.Students.Find(student.Id);
                    if (existingStudent != null)
                    {
                        existingStudent.Name = student.Name;
                        existingStudent.City = student.City;
                    }
                }
                else
                {
                    db.Students.Add(student);
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // Delete student by ID
        public ActionResult Delete(int id)
        {
            using (var db = new MydbEntities())
            {
                var student = db.Students.Find(id);
                if (student != null)
                {
                    db.Students.Remove(student);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }

        // Delete selected students
        [HttpPost]
        public ActionResult DeleteSelected(int[] selectedIds)
        {
            using (var db = new MydbEntities())
            {
                if (selectedIds != null)
                {
                    foreach (var id in selectedIds)
                    {
                        var student = db.Students.Find(id);
                        if (student != null)
                        {
                            db.Students.Remove(student);
                        }
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }
    }
}
