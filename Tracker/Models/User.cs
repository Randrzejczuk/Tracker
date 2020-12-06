using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }

        public List<Issue> IssuesAgent { get; set; }
        public List<Issue> IssuesNotifier { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}