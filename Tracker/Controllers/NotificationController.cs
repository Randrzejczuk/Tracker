using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.ViewModels;
using System.Data.Entity;

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
                Workers = context.Users.Where(x=>x.CompanyId==1).ToList(),
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
                viewModel.Notification.StartTime = viewModel.Notification.StartTime.AddHours(int.Parse(viewModel.StartTime.Substring(0, 2)));
                viewModel.Notification.StartTime = viewModel.Notification.StartTime.AddMinutes(int.Parse(viewModel.StartTime.Substring(3, 2)));
                viewModel.Notification.EndTime = viewModel.Date;
                if (viewModel.EndTime == "00:00")
                    viewModel.Notification.EndTime = viewModel.Notification.EndTime.AddDays(1);
                else
                {
                    viewModel.Notification.EndTime = viewModel.Notification.EndTime.AddHours(int.Parse(viewModel.EndTime.Substring(0, 2)));
                    viewModel.Notification.EndTime = viewModel.Notification.EndTime.AddMinutes(int.Parse(viewModel.EndTime.Substring(3, 2)));
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

        public ActionResult Delete(Notification notification)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            Notification notificationToDelete = context.Notifications.Where(x => x.Id == notification.Id).FirstOrDefault();
            int issue = notificationToDelete.IssueId;
            context.Notifications.Remove(notificationToDelete);
            context.SaveChanges();
            return RedirectToAction("Details","Issue", new { issueId = issue });
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
                StartTime = notificationToEdit.StartTime.ToString("hh:mm"),
                EndTime = notificationToEdit.EndTime.ToString("hh:mm")
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
                notificationToEdit.StartTime = notificationToEdit.StartTime.AddHours(int.Parse(viewModel.StartTime.Substring(0, 2)));
                notificationToEdit.StartTime = notificationToEdit.StartTime.AddMinutes(int.Parse(viewModel.StartTime.Substring(3, 2)));
                notificationToEdit.EndTime = viewModel.Date;
                if (viewModel.EndTime == "00:00")
                    notificationToEdit.EndTime = notificationToEdit.EndTime.AddDays(1);
                else
                {
                    notificationToEdit.EndTime = notificationToEdit.EndTime.AddHours(int.Parse(viewModel.EndTime.Substring(0, 2)));
                    notificationToEdit.EndTime = notificationToEdit.EndTime.AddMinutes(int.Parse(viewModel.EndTime.Substring(3, 2)));
                }
                context.SaveChanges();
                return RedirectToAction("Details","Issue", new { issueId = viewModel.Notification.IssueId });
            }
            else 
            {
                viewModel.Workers = context.Users.Where(x => x.CompanyId == 1).ToList();
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