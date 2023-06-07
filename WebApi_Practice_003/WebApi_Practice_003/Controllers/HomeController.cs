using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApi_Practice_003.Models;

namespace WebApi_Practice_003.Controllers
{ 
    public class HomeController : Controller
    {
        FoodRepository fp = new FoodRepository();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.FoodProductsData= fp.GetAll().ToList();
            return View();
        }
    }
}
