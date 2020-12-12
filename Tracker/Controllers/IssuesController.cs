using System.Linq;
using System.Web.Mvc;
using Tracker.Models;

namespace Tracker.Controllers
{
    public class IssuesController : Controller
    {
        // GET: Issues/List
        public ViewResult List()
        {
            using (var context = new TrackerDbContext())
            {
                Issue issue = context.Issues.FirstOrDefault();
                return View(issue);
            }
        }
        public ViewResult Create()
        {
            Issue issue = new Issue();
            return View(issue);
        }
    }
}