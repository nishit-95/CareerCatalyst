using CareerCatalyst.Models;
using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerCatalyst.Controllers
{
    public class UserController : Controller
    {
        private CareerCatalystDBEntities Db = new CareerCatalystDBEntities();

        // GET: User
        public ActionResult NewUser()
        {
            return View(new UserMV());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewUser(UserMV UserMV)
        {

            return View(UserMV);
        }
    }
}