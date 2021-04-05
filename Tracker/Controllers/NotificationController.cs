using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.ViewModels;
using System.Data.Entity;
using System;

namespace Tracker.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification

        public ActionResult Create(int issueId)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            CreateNotificationViewModel createNotificationViewModel = new CreateNotificationViewModel()
            {
                Workers = context.Users.Where(x => x.ArchivedTimeStamp == null).Where(x=>x.CompanyId==1).ToList(),
                Notification = new Notification()
                {
                    IssueId = issueId
                }
            };
            return View(createNotificationViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNotificationViewModel viewModel)
        {
            var context = new TrackerDbContext();
            if (ModelState.IsValid)
            {
                viewModel.Notification.StartTime = viewModel.Date;
                viewModel.Notification.StartTime = viewModel.Notification.StartTime.Add(TimeSpan.Parse(viewModel.StartTime.Substring(0, 5)));
                viewModel.Notification.EndTime = viewModel.Date;
                if (viewModel.EndTime == "00:00")
                    viewModel.Notification.EndTime = viewModel.Notification.EndTime.AddDays(1);
                else
                {
                    viewModel.Notification.EndTime = viewModel.Notification.EndTime.Add(TimeSpan.Parse(viewModel.EndTime.Substring(0, 5)));
                }
                context.Notifications.Add(viewModel.Notification);
                context.SaveChanges();
                return RedirectToAction("Details","Issue", new { issueId = viewModel.Notification.IssueId });
            }
            else
            {
                viewModel.Workers = context.Users.Where(x => x.CompanyId == 1).ToList();
                return View(viewModel);
            }
        }

        public ActionResult Edit(int notificationId)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            Notification notificationToEdit = context.Notifications.Where(x => x.Id == notificationId).FirstOrDefault();
            CreateNotificationViewModel notificationViewModel = new CreateNotificationViewModel
            {
                Workers = context.Users.Where(x => x.CompanyId == 1).ToList(),
                Notification = notificationToEdit,
                Date = notificationToEdit.StartTime.Date,
                StartTime = notificationToEdit.StartTime.ToString("HH:mm"),
                EndTime = notificationToEdit.EndTime.ToString("HH:mm")
            };
            return View(notificationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int notificationId, CreateNotificationViewModel viewModel)
        {
            var context = new TrackerDbContext();
            if (ModelState.IsValid)
            {
                Notification notificationToEdit = context.Notifications.Where(x => x.Id == notificationId).FirstOrDefault();
                notificationToEdit.WorkerId = viewModel.Notification.WorkerId;
                notificationToEdit.WorkDone = viewModel.Notification.WorkDone;

                notificationToEdit.StartTime = viewModel.Date;
                notificationToEdit.StartTime = notificationToEdit.StartTime.Add(TimeSpan.Parse(viewModel.StartTime.Substring(0, 5)));

                notificationToEdit.EndTime = viewModel.Date;
                if (viewModel.EndTime == "00:00")
                    notificationToEdit.EndTime = notificationToEdit.EndTime.AddDays(1);
                else
                {
                    notificationToEdit.EndTime = notificationToEdit.EndTime.Add(TimeSpan.Parse(viewModel.EndTime.Substring(0, 5)));
                }
                context.SaveChanges();
                return RedirectToAction("Details","Issue", new { issueId = viewModel.Notification.IssueId });
            }
            else 
            {
                viewModel.Workers = context.Users.Where(x => x.ArchivedTimeStamp == null).Where(x => x.CompanyId == 1).ToList();
                return View(viewModel);
            }
        }
        public ActionResult Details(int notificationId)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            Notification notification = context.Notifications
                .Where(x => x.Id == notificationId)
                .Include(x => x.Issue)
                .Include(x => x.Worker)
                .FirstOrDefault();
            return View(notification);
        }
    }
}