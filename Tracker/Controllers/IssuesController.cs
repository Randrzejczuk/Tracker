using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracker.Models;
using System.Web.Mvc;

namespace Tracker.Controllers
{
    public class IssuesController : Controller
    {
        // GET: Issues/List
        public ViewResult List()
        {
            var issue = new Issue()
            {
                Id = 1,
                Title = "Test"
            };
            return View(issue);
        }
    }
}