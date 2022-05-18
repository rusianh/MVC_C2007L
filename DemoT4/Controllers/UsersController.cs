using DemoT4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoT4.Controllers
{
    public class UsersController : Controller
    {
        public static List<User> list = new List<User>()
        {
            new User(){ Name = "Huy", Email="huy@",Id=1, Password="huy123"},
            new User(){ Name = "Phuong", Email = "Phuong@yahoo.com", Id=2, Password="phuong123"}
        };
        // GET: Users
        public ActionResult Index()
        {

            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(User _user)
        {
            list.Add(_user);
            //return View("index",list);
            return RedirectToAction("Index",list);
        }
    }
}