using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Practice2_S5_DataAnnotation.Models;

namespace Practice2_S5_DataAnnotation.Controllers
{
    public class UsersController : Controller
    {
        public static List<User> list = new List<User>()
        {
            new User(){Name = "Cris", Email = "Cris@yahoo.com"}
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
        public ActionResult Create(User user)
        {
            list.Add(user);
            return View("Details",user);
        }
        public ActionResult Details(User user)
        {
            //var user = list.Find(x => x.Name == name).;
            return View(user);
        }
        //void SearchDuLieu()
        //{
        //    List<User> list = originalList;
        //    if (!string.IsNullOrEmpty(txtKeyword.Text))
        //        list = list.Where(item => item.Username.Contains(txtKeyword.Text) || item.FullName.Contains(txtKeyword.Text) || item.Email.Contains(txtKeyword.Text)).ToList();
        //    if (!string.IsNullOrEmpty(cbSearchType.Text))
        //        list = list.Where(item => item.UserType == cbSearchType.Text).ToList();
        //    dgvUser.DataSource = list;
        //}
    }
}