using System;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public class Notification
    {
        public int Id { get; set; }
        [Display(Name = "Wykonana praca")]
        [Required(ErrorMessage = "Wymagane jest podanie wykonanej pracy.")]
        public string WorkDone { get; set; }
        [Display(Name = "Czas rozpoczęcia pracy")]
        [Required(ErrorMessage = "Wymagane jest podanie czasu rozpoczęcia pracy.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Czas zakończenia pracy")]
        [Required(ErrorMessage = "Wymagane jest podanie czasu zakończenia pracy.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]  
        public DateTime EndTime { get; set; }

        public int IssueId { get; set; }
        public Issue Issue { get; set; }
        public int WorkerId { get; set; }
        public User Worker { get; set; }

        [Display(Name ="Czas pracy")]
        public TimeSpan WorkTime
        {
            get
            {
                return EndTime - StartTime;
            }
        }
    }
}