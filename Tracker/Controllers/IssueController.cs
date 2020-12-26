using System.Linq;
using System.Web.Mvc;
using Tracker.Models;
using Tracker.ViewModels;

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
                issue.Company = context.Companies.Where(x => x.Id == issue.Companyid).FirstOrDefault();
                issue.Agent = context.Users.Where(x => x.Id == issue.AgentId).FirstOrDefault();
                issue.Notifier = context.Users.Where(x => x.Id == issue.NotifierId).FirstOrDefault();
                issue.StatusId = 1;
                issue.Status = context.Statuses.Where(x => x.Id == issue.StatusId).FirstOrDefault();
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
            var issues = context.Issues;
            return View(issues);
        }
    }
}