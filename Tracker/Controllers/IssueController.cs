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
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
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

        public ActionResult List()
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
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
            context.Notifications.RemoveRange(issueToDelete.Notifications);
            context.Issues.Remove(issueToDelete);
            context.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Edit(int issueId)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            CreateIssueViewModel IssueViewModel = new CreateIssueViewModel()
            {
                Companies = context.Companies.ToList(),
                Users = context.Users.ToList(),
                Agents = context.Users.Where(x => x.CompanyId == 1).ToList(),
                Issue = context.Issues.Where(x=>x.Id==issueId).FirstOrDefault()
            };
            return View(IssueViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int issueId, Issue issue)
        {
            var context = new TrackerDbContext();
            if (ModelState.IsValid)
            {
                Issue issueToEdit = context.Issues.Where(x => x.Id == issueId).FirstOrDefault();
                issueToEdit.Title = issue.Title;
                issueToEdit.AgentId = issue.AgentId;
                issueToEdit.NotifierId = issue.NotifierId;
                issueToEdit.Companyid = issueToEdit.Companyid;
                context.SaveChanges();
                return RedirectToAction("List","Notification", new { issueId });
            }
            CreateIssueViewModel issueViewModel = new CreateIssueViewModel()
            {
                Companies = context.Companies.ToList(),
                Users = context.Users.ToList(),
                Agents = context.Users.Where(x => x.CompanyId == 1).ToList(),
                Issue = issue
            };
            return View(issueViewModel);
        }
    }
}