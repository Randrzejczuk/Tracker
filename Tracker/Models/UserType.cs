using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public class UserType
    {
        public int Id { get; set; }
        [Display(Name = "Typ użytkownika")]
        [Required(ErrorMessage = "Wymagane jest podanie typu użytkownika.")]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}