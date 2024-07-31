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
                var checkuser = Db.UserTables.Where(u=>u.EmailAddress == UserMV.EmailAddress).FirstOrDefault();
                if (checkuser != null) {
                    ModelState.AddModelError("EmailAddress", "Email is already Registered");
                    return View(UserMV);
                }

                checkuser = Db.UserTables.Where(u => u.UserName == UserMV.UserName).FirstOrDefault();
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
                            company.Logo = "~/Content/assets/img/logo.png";
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
        public ActionResult Login()
        {
            return View(new UserLoginMV());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginMV userLoginMV)
        {
            if (ModelState.IsValid) { 
                var user = Db.UserTables.Where(u=>u.UserName == userLoginMV.UserName && u.password == userLoginMV.Password).FirstOrDefault();
                if (user == null) {
                    ModelState.AddModelError(string.Empty, "UserName and Paswword is Incorrect");
                    return View(userLoginMV);
                }
                Session["UserID"] = user.UserID;
                Session["UserName"] = user.UserName;
                Session["UserTypeID"] = user.UserTypeID;

                if (user.UserTypeID == 2) {
                    Session["CompanyID"] = user.CompanyTables.FirstOrDefault().CompanyID;
                }

                return RedirectToAction("Index", "Home");
            }
            return View(userLoginMV);
        }
        public ActionResult Logout()
        {
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["CompanyID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            return RedirectToAction("Index","Home");
        }

        public ActionResult AllUsers()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
            {
                return RedirectToAction("Login", "User");
            }
            var users = Db.UserTables.ToList();
            return View(users);
        }
    }
}