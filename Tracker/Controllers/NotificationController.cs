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
                return RedirectToAction("List", new { issueId = viewModel.Notification.IssueId });
            }
            else
            {
                viewModel.Workers = context.Users.Where(x => x.CompanyId == 1).ToList();
                return View(viewModel);
            }
        }

        public ActionResult List(int issueId)
        {
            var context = new TrackerDbContext();
            Issue issue = context.Issues.Where(x => x.Id == issueId).Include(x=>x.Notifications).FirstOrDefault();
            return View(issue);
        }
    }
}