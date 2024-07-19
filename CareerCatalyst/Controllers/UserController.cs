using CareerCatalyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CareerCatalyst.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult NewUser()
        {
            return View(new UserMV());
        }
    }
}