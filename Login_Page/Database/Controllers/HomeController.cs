using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Database.Models;

namespace Database.Controllers
{
    public class HomeController : Controller
    {
        // Action for the Index page
        public ActionResult Index()
        {
            return View();
        }

        // Action for the About page
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        // Action for the Contact page
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        // GET: LoginPage
        public ActionResult LoginPage(int? id)
        {
            Student student = new Student();

            if (id.HasValue && id.Value != 0)
            {
                using (MyDbEntities db = new MyDbEntities())
                {
                    student = db.Students.Find(id) ?? new Student();
                }
            }

            // Pass any error message to the view
            ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;

            return View(student);
        }

        // POST: LoginPage
        [HttpPost]
        public ActionResult LoginPage(Student s)
        {
            using (MyDbEntities db = new MyDbEntities())
            {
                var usernameExists = db.Students.Any(x => x.UserName == s.UserName && x.Id != s.Id);

                if (usernameExists)
                {
                    TempData["ErrorMessage"] = "This username already exists. Please enter a different username.";
                    return RedirectToAction("LoginPage", new { id = s.Id });
                }

                if (s.Id != 0)
                {
                    // Edit existing student
                    Student ss = db.Students.Find(s.Id);
                    if (ss != null)
                    {
                        ss.UserName = s.UserName;
                        ss.Password = s.Password;
                        ss.Fname = s.Fname;
                        ss.Lname = s.Lname;
                        ss.City = s.City;
                        ss.Gender = s.Gender;
                    }
                }
                else
                {
                    // Add new student
                    db.Students.Add(s);
                }

                db.SaveChanges();
            }
            return RedirectToAction("Table");
        }

        // Action for Table view
        public ActionResult Table()
        {
            using (MyDbEntities db = new MyDbEntities())
            {
                List<Student> students = db.Students.ToList(); // Fetching from the database
                return View(students);
            }
        }

        // Action for deleting a student
        public ActionResult Delete(int id)
        {
            using (MyDbEntities db = new MyDbEntities())
            {
                Student stud = db.Students.Find(id);
                if (stud != null)
                {
                    db.Students.Remove(stud);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Table"); // Redirect to Table view after deletion
        }

        // POST: DeleteSelected
        [HttpPost]
        public ActionResult DeleteSelected(int[] selectedIds)
        {
            using (MyDbEntities db = new MyDbEntities())
            {
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
            }
            return RedirectToAction("Table"); // Redirect to Table view after deletion
        }
    }
}
