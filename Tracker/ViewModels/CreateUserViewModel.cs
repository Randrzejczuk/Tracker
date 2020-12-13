using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tracker.Models;

namespace Tracker.ViewModels
{
    public class CreateUserViewModel
    {
        public User User { get; set; }
        public List<Company> Companies { get; set; }
        public List<UserType> UserTypes { get; set; }
    }
}