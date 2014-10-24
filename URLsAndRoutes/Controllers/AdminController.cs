using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace URLsAndRoutes.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Controller = "Admin";
            ViewBag.Action = "Index";

            return View("ActionName");
        }

        [Route("Users/Add/{user}/{password}"]
        public string ChangePass(string user, string password){
            return string.Format("ChangePass ");
        }

    }
}