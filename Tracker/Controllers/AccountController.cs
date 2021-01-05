using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracker.ViewModels;
using System.Web.Mvc;
using System.DirectoryServices.AccountManagement;
using Tracker.Models;
using System.Data.Entity;

namespace Tracker.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            var loginUserViewModel = new LoginUserViewModel();
            return View(loginUserViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUserViewModel viewModel)
        {
            bool isVAlid;
            /*using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, null))
            {
               isVAlid = pc.ValidateCredentials(viewModel.Email, viewModel.Password);
            }*/

            using (PrincipalContext pc = new PrincipalContext(ContextType.Machine, null))
            {
                isVAlid = pc.ValidateCredentials(viewModel.Login,viewModel.Password);
            }
            if(isVAlid && ModelState.IsValid)
            {
                var context = new TrackerDbContext();
                try
                {
                    var users = context.Users;
                    foreach(var user in users)
                    {
                        if (user.Login==viewModel.Login)
                        {
                            Session["User"] = user;
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    return View(viewModel);
                }
                catch
                {
                    return View(viewModel);
                }
            }
            else
            return View(viewModel);
        }
    }
}