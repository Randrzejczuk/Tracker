﻿using System.Linq;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.ViewModels;

namespace Tracker.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Create()
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            CreateUserViewModel userViewModel = new CreateUserViewModel
            {
                Companies = context.Companies.ToList(),
                UserTypes = context.UserTypes.ToList()
            };
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            var context = new TrackerDbContext();
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("List");
            }
            CreateUserViewModel userViewModel = new CreateUserViewModel
            {
                Companies = context.Companies.ToList(),
                UserTypes = context.UserTypes.ToList(),
                User = user
            };
            return View(userViewModel);
        }

        public ActionResult List()
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            var users = context.Users;
            return View(users);
        }

        public ActionResult Delete(User user)
        {
            var context = new TrackerDbContext();
            User userToDelete = context.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            context.Users.Remove(userToDelete);
            context.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Edit(int userId)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            CreateUserViewModel userViewModel = new CreateUserViewModel
            {
                Companies = context.Companies.ToList(),
                UserTypes = context.UserTypes.ToList(),
                User = context.Users.Where(x => x.Id == userId).FirstOrDefault()
            };
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int userId,User user)
        {         
            var context = new TrackerDbContext();
            if (ModelState.IsValid)
            {
                User userToEdit = context.Users.Where(x => x.Id == userId).FirstOrDefault();
                userToEdit.FirstName = user.FirstName;
                userToEdit.Lastname = user.Lastname;
                userToEdit.Login = user.Login;
                userToEdit.Password = user.Password;
                userToEdit.PhoneNumber = user.PhoneNumber;
                userToEdit.Email = user.Email;
                userToEdit.CompanyId = user.CompanyId;
                userToEdit.UserTypeId = userToEdit.UserTypeId;
                context.SaveChanges();
                return RedirectToAction("List");
            }
            CreateUserViewModel userViewModel = new CreateUserViewModel
            {
                Companies = context.Companies.ToList(),
                UserTypes = context.UserTypes.ToList(),
                User = context.Users.Where(x => x.Id == userId).FirstOrDefault()
            };
            return View(userViewModel);
        }
    }
}
