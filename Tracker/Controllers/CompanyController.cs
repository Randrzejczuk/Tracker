using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker.Models;

namespace Tracker.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                var context = new TrackerDbContext();
                context.Companies.Add(company);
                context.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }

        public ActionResult List()
        {
            var context = new TrackerDbContext();
            var companies = context.Companies;
            companies.OrderBy(x=>x.Name);
            return View(companies);
        }

        public ActionResult Delete(Company company)
        {
            var context = new TrackerDbContext();
            Company companyToDelete = context.Companies.Where(x => x.Id == company.Id).FirstOrDefault();
            context.Companies.Remove(companyToDelete);
            context.SaveChanges();
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int companyId)
        {
            var context = new TrackerDbContext();
            Company company = context.Companies.Where(x => x.Id == companyId).FirstOrDefault();
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int companyId,Company company)
        {
            if (ModelState.IsValid)
            {
                var context = new TrackerDbContext();
                Company companyToEdit = context.Companies.Where(x => x.Id == companyId).FirstOrDefault();
                companyToEdit.Name = company.Name;
                context.SaveChanges();
                return RedirectToAction("List");
            }
            return View();
        }
    }
}