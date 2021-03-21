using System.Linq;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.ViewModels;
using System.Data.Entity;
using System;

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

        public ActionResult List(string sortOrder, string searchString)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            var issues = context.Issues
                .Include(x => x.Status)
                .Include(x => x.Notifier)
                .Include(x => x.Agent)
                .ToList();

            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AgentSortParm = sortOrder == "agent" ? "agent_desc" : "agent";
            ViewBag.NotifierSortParm = sortOrder == "notifier" ? "notifier_desc" : "notifier";
            ViewBag.StatusSortParm = sortOrder == "status" ? "status_desc" : "status";

            if (!String.IsNullOrEmpty(searchString))
            {
                issues = issues.Where(x => x.Title.ToLower().Contains(searchString.ToLower())
                || x.Agent.FullName.ToLower().Contains(searchString.ToLower())
                || x.Notifier.FullName.ToLower().Contains(searchString.ToLower())
                || x.Status.Name.ToLower().Contains(searchString.ToLower())
                ).ToList();
                ViewBag.IssueSearchString = searchString;
            }

            switch (sortOrder)
            {
                case "agent":
                    issues = issues.OrderBy(x => x.Agent.Lastname).ToList();
                    break;
                case "agent_desc":
                    issues = issues.OrderByDescending(x => x.Agent.Lastname).ToList();
                    break;
                case "notifier":
                    issues = issues.OrderBy(x => x.Notifier.Lastname).ToList();
                    break;
                case "notifier_desc":
                    issues = issues.OrderByDescending(x => x.Notifier.Lastname).ToList();
                    break;
                case "status":
                    issues = issues.OrderBy(x => x.Status.Name).ToList();
                    break;
                case "status_desc":
                    issues = issues.OrderByDescending(x => x.Status.Name).ToList();
                    break;
                case "title_desc":
                    issues = issues.OrderByDescending(x => x.Title).ToList();
                    break;
                default:
                    issues = issues.OrderBy(x => x.Title).ToList();
                    break;
            }
            return View(issues);
        }

        public ActionResult Delete(Issue issue)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
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
                return RedirectToAction("List");
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
        public ActionResult Details(int issueId, string sortOrder, string searchString)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            Issue issue = context.Issues.Where(x => x.Id == issueId)
                .Include(x => x.Notifications)
                .Include(x => x.Agent)
                .Include(x => x.Notifier)
                .Include(x => x.Status)
                .Include(x => x.Company)
                .FirstOrDefault();

            ViewBag.IssueId = issueId;
            ViewBag.WorkerSortParm = String.IsNullOrEmpty(sortOrder) ? "worker_desc" : "";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.WorkTimeSortParm = sortOrder == "worktime" ? "worktime_desc" : "worktime";

            if (!String.IsNullOrEmpty(searchString))
            {
                issue.Notifications = issue.Notifications.Where(x => x.Worker.FullName.ToLower().Contains(searchString.ToLower()))
                    .ToList();
                ViewBag.NotificationSearchString = searchString;
            }

            switch (sortOrder)
            {
                case "date":
                    issue.Notifications = issue.Notifications.OrderBy(x => x.StartTime.Date).ToList();
                    break;
                case "date_desc":
                    issue.Notifications = issue.Notifications.OrderByDescending(x => x.StartTime.Date).ToList();
                    break;
                case "worktime":
                    issue.Notifications = issue.Notifications.OrderBy(x => x.WorkTime).ToList();
                    break;
                case "worktime_desc":
                    issue.Notifications = issue.Notifications.OrderByDescending(x => x.WorkTime).ToList();
                    break;
                case "worker_desc":
                    issue.Notifications = issue.Notifications.OrderByDescending(x => x.Worker.Lastname).ToList();
                    break;
                default:
                    issue.Notifications = issue.Notifications.OrderBy(x => x.Worker.Lastname).ToList();
                    break;
            }

            return View(issue);
        }
    }
}