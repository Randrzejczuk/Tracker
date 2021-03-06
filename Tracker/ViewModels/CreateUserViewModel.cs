﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tracker.Models;

namespace Tracker.ViewModels
{
    public class CreateUserViewModel
    {
        public User User { get; set; }
        [Display(Name = "Firma")]
        public List<Company> Companies { get; set; }
        [Display(Name = "Typ użytkownika")]
        public List<UserType> UserTypes { get; set; }
    }
}