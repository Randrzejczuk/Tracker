using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
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

        public ActionResult List(string sortOrder)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");

            var context = new TrackerDbContext();
            var companies = context.Companies.ToList();

            ViewBag.CompanySortParm = String.IsNullOrEmpty(sortOrder) ? "company_desc" : "";

            switch (sortOrder)
            {
                case "company_desc":
                    companies = companies.OrderByDescending(x => x.Name).ToList();
                    break;
                default:
                    companies = companies.OrderBy(x => x.Name).ToList();
                    break;
            }
            return View(companies);
        }

        public ActionResult Delete(Company company)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
            var context = new TrackerDbContext();
            if (company.Id != 1)
            {
                Company companyToDelete = context.Companies.Where(x => x.Id == company.Id).FirstOrDefault();
                context.Companies.Remove(companyToDelete);
                context.SaveChanges();
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int companyId)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");
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
        public ActionResult Details(int companyId, string sortOrder)
        {
            if (Session["User"] == null)
                return RedirectToAction("Login", "Account");

            var context = new TrackerDbContext();
            Company company = context.Companies
                .Where(x => x.Id == companyId)
                .Include(x => x.Employees)
                .FirstOrDefault();

            ViewBag.CompanyId = companyId;
            ViewBag.LastNameSortParm = String.IsNullOrEmpty(sortOrder) ? "lname_desc" : "";
            ViewBag.FirstNameSortParm = sortOrder == "fname" ? "fname_desc" : "fname";
            ViewBag.CompanySortParm = sortOrder == "company" ? "company_desc" : "company";

            switch (sortOrder)
            {
                case "fname":
                    company.Employees = company.Employees.OrderBy(x => x.FirstName).ToList();
                    break;
                case "fname_desc":
                    company.Employees = company.Employees.OrderByDescending(x => x.FirstName).ToList();
                    break;
                case "company":
                    company.Employees = company.Employees.OrderBy(x => x.Company.Name).ToList();
                    break;
                case "company_desc":
                    company.Employees = company.Employees.OrderByDescending(x => x.Company.Name).ToList();
                    break;
                case "lname_desc":
                    company.Employees = company.Employees.OrderByDescending(x => x.Lastname).ToList();
                    break;
                default:
                    company.Employees = company.Employees.OrderBy(x => x.Lastname).ToList();
                    break;
            }

            
            return View(company);
        }
    }
}