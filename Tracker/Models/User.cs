using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Wymagane jest podanie imienia.")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Wymagane jest podanie nazwiska.")]
        public string Lastname { get; set; }
        [Display(Name = "Login")]
        public string Login { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Wymagany prawidłowy jest adres Email.")]
        public string Email { get; set; }
        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }
        public DateTime? ArchivedTimeStamp { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int UserTypeId { get; set; }
        public UserType UserType { get; set; }

        public List<Notification> Notifications { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {Lastname}";
            }
        }

        public void Archive()
        {
            ArchivedTimeStamp = DateTime.Now;
        }
    }
}