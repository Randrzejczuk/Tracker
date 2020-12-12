using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Display (Name="Nazwa firmy")]
        [Required(ErrorMessage ="Wymagane jest podanie nazwy firmy.")]
        public string Name { get; set; }

        public List<Issue> Issues { get; set; }
        public List<User> Employees { get; set; }
    }
}