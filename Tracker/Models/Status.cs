using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public class Status
    {
        public int Id { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessage = "Wymagane jest podanie statusu.")]
        public string Name { get; set; }

        public List<Issue> Issues { get; set; }
    }
}