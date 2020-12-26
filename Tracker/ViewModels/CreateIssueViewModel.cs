using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tracker.Models;

namespace Tracker.ViewModels
{
    public class CreateIssueViewModel
    {
        public Issue Issue { get; set; }
        [Display(Name = "Firma")]
        public List<Company> Companies { get; set; }
        [Display(Name = "Zgloszone przez")]
        public List<User> Users { get; set; }
        [Display(Name = "Agent")]
        public List<User> Agents { get; set; }
    }
}