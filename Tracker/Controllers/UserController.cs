using System;
using System.Data.Entity;
using System.Linq;
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

        public ActionResult List(string sortOrder)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");

            var context = new TrackerDbContext();
            var users = context.Users
                .Include(x => x.Company)
                .ToList();

            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "fname" ? "fname_desc" : "fname";
            ViewBag.CompanySortParm = sortOrder == "company" ? "company_desc" : "company";

            switch (sortOrder)
            {
                case "fname":
                    users = users.OrderBy(x => x.FirstName).ToList();
                    break;
                case "fname_desc":
                    users = users.OrderByDescending(x => x.FirstName).ToList();
                    break;
                case "company":
                    users = users.OrderBy(x => x.Company.Name).ToList();
                    break;
                case "company_desc":
                    users = users.OrderByDescending(x => x.Company.Name).ToList();
                    break;
                case "lname_desc":
                    users = users.OrderByDescending(x => x.Lastname).ToList();
                    break;
                default:
                    users = users.OrderBy(x => x.Lastname).ToList();
                    break;
            }
            return View(users);
        }

        public ActionResult Delete(User user)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
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
        public ActionResult Details(int userId, string sortOrder)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            User user = context.Users
                .Where(x => x.Id == userId)
                .Include(x=>x.Notifications)
                .Include(x => x.Company)
                .Include(x => x.UserType)
                .FirstOrDefault();

            ViewBag.UserId = userId;
            ViewBag.WorkerSortParm = String.IsNullOrEmpty(sortOrder) ? "worker_desc" : "";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.WorkTimeSortParm = sortOrder == "worktime" ? "worktime_desc" : "worktime";


            switch (sortOrder)
            {
                case "date":
                    user.Notifications = user.Notifications.OrderBy(x => x.StartTime.Date).ToList();
                    break;
                case "date_desc":
                    user.Notifications = user.Notifications.OrderByDescending(x => x.StartTime.Date).ToList();
                    break;
                case "worktime":
                    user.Notifications = user.Notifications.OrderBy(x => x.WorkTime).ToList();
                    break;
                case "worktime_desc":
                    user.Notifications = user.Notifications.OrderByDescending(x => x.WorkTime).ToList();
                    break;
                case "worker_desc":
                    user.Notifications = user.Notifications.OrderByDescending(x => x.Worker.Lastname).ToList();
                    break;
                default:
                    user.Notifications = user.Notifications.OrderBy(x => x.Worker.Lastname).ToList();
                    break;
            }

            return View(user);
        }
    }
}
