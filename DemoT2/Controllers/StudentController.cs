using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoT2.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            ViewData["Name"] = "Cris";
            ViewData["Age"] = "18";
            ViewData["Adress"] = "Hanoi";
            return View("ShowData");
        }
        public ActionResult ShowData()
        {
            return View();
        }
    }
}