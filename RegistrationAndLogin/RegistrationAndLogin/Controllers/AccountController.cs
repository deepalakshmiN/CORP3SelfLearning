using RegistrationAndLogin.Models;
using RegistrationAndLogin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RegistrationAndLogin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRegisterDetails(Register registerDetails)
        {
            if (ModelState.IsValid)
            {
                using (var databaseContext = new SqlCodeFirstMvcDbEntities())
                {
                    RegisterUser reglog = new RegisterUser();


                    reglog.FirstName = registerDetails.FirstName;
                    reglog.LastName = registerDetails.LastName;
                    reglog.Email = registerDetails.Email;
                    reglog.Password = registerDetails.Password;


                    databaseContext.RegisterUsers.Add(reglog);
                    databaseContext.SaveChanges();
                }

                ViewBag.Message = "User Details Saved";
                return View("Register");
            }
            else
            {

                return View("Register", registerDetails);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var isValidUser = IsValidUser(model);

                if (isValidUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }

        public RegisterUser IsValidUser(LoginViewModel model)
        {
            using (var dataContext = new SqlCodeFirstMvcDbEntities())
            {
                RegisterUser user = dataContext.RegisterUsers.Where(query => query.Email.Equals(model.Email) && query.Password.Equals(model.Password)).SingleOrDefault();
                if (user == null)
                    return null;
                else
                    return user;
            }
        }


    }
}