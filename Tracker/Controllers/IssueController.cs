using System.Linq;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.ViewModels;
using System.Data.Entity;

namespace Tracker.Controllers
{
    public class IssueController : Controller
    {
        
        public ActionResult Create()
        {
            var context = new TrackerDbContext();
            CreateIssueViewModel createIssueViewModel = new CreateIssueViewModel()
            {
                Companies = context.Companies.ToList(),
                Users = context.Users.ToList(),
                Agents = context.Users.Where(x=>x.CompanyId==1).ToList()
            };
            return View(createIssueViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Issue issue)
        {
            var context = new TrackerDbContext();
            if (ModelState.IsValid)
            {
                issue.StatusId = 1;
                context.Issues.Add(issue);
                context.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                CreateIssueViewModel createIssueViewModel = new CreateIssueViewModel()
                {
                    Companies = context.Companies.ToList(),
                    Users = context.Users.ToList(),
                    Agents = context.Users.Where(x => x.CompanyId == 1).ToList(),
                    Issue = issue
                };
                return View(createIssueViewModel);
            }
        }

        public ViewResult List()
        {
            var context = new TrackerDbContext();
            var issues = context.Issues
                .Include(x => x.Status)
                .Include(x=>x.Notifier)
                .Include(x=>x.Agent);
            return View(issues);
        }

        public ActionResult Delete(Issue issue)
        {
            var context = new TrackerDbContext();
            Issue issueToDelete = context.Issues.Where(x => x.Id == issue.Id).Include(x=>x.Notifications).FirstOrDefault();
            foreach (var notification in issueToDelete.Notifications)
                context.Notifications.Remove(notification);
            context.Issues.Remove(issueToDelete);
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}