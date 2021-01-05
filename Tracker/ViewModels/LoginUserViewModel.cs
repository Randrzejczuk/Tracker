using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tracker.ViewModels
{
    public class LoginUserViewModel
    {
        private string Domain = "";

        [Display(Name = "Login")]
        [Required(ErrorMessage = "Wymagane jest podanie loginu.")]
        public string Login { get; set; }
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Wymagane jest podanie hasła.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public string Email
        {
            get
            {
                return ($"{Login}@{Domain}.com");
            }
        }
    }
}