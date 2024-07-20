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
            if (ModelState.IsValid) { 
                var checkuser = Db.UserTables.Where(u=>u.EmailAddress == UserMV.EmailAddress);
                if (checkuser != null) {
                    ModelState.AddModelError("EmailAddress", "Email is already Registered");
                    return View(UserMV);
                }

                checkuser = Db.UserTables.Where(u => u.UserName == UserMV.UserName);
                if (checkuser != null)
                {
                    ModelState.AddModelError("UserName", "UserName is already Registered");
                    return View(UserMV);
                }
                using (var trans = Db.Database.BeginTransaction())
                {

                    try
                    {
                        var user = new UserTable();
                        user.UserName = UserMV.UserName;
                        user.password = UserMV.password;
                        user.ContactNo = UserMV.ContactNo;
                        user.EmailAddress = UserMV.EmailAddress;
                        user.UserTypeID = UserMV.AreYouProvider == true ? 2 : 3;
                        Db.UserTables.Add(user);
                        Db.SaveChanges();

                        if (UserMV.AreYouProvider == true)
                        {

                            var company = new CompanyTable();
                            company.UserID = user.UserID;

                            if (string.IsNullOrEmpty(UserMV.Company.EmailAddress))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.EmailAddress", "Required!*");
                                return View(UserMV);
                            }
                            if (string.IsNullOrEmpty(UserMV.Company.CompanyName))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.CompanyName", "Required!*");
                                return View(UserMV);
                            }
                            if (string.IsNullOrEmpty(UserMV.Company.PhoneNo))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.PhoneNo", "Required!*");
                                return View(UserMV);
                            }
                            if (string.IsNullOrEmpty(UserMV.Company.Description))
                            {
                                trans.Rollback();
                                ModelState.AddModelError("Company.Description", "Required!*");
                                return View(UserMV);
                            }


                            company.EmailAddress = UserMV.Company.EmailAddress;
                            company.CompanyName = UserMV.Company.CompanyName;
                            company.ContactNo = UserMV.ContactNo;
                            company.PhoneNo = UserMV.Company.PhoneNo;
                            company.Logo = "~/Content/assets/img/logo.logo.png";
                            company.Description = UserMV.Company.Description;
                            Db.CompanyTables.Add(company);
                            Db.SaveChanges();
                        }
                        trans.Commit();
                        return RedirectToAction("Login");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError(string.Empty, "Please provide correct details");
                        trans.Rollback();
                    }

                }
            }
            return View(UserMV);
        }
    }
}