using System;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public class Notification
    {
        // Id of the notification.
        public int Id { get; set; }
        // Description of employee's work.
        [Display(Name = "Wykonana praca")]
        [Required(ErrorMessage = "Wymagane jest podanie wykonanej pracy.")]
        public string WorkDone { get; set; }
        // Time and date when employee started working.
        [Display(Name = "Czas rozpoczęcia pracy")]
        [Required(ErrorMessage = "Wymagane jest podanie czasu rozpoczęcia pracy.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime StartTime { get; set; }
        // Time and date when employee finished working.
        [Display(Name = "Czas zakończenia pracy")]
        [Required(ErrorMessage = "Wymagane jest podanie czasu zakończenia pracy.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]  
        public DateTime EndTime { get; set; }
        // Time of archivisation of notification
        public DateTime? ArchivedTimeStamp { get; set; }

        // Id of the issue.
        public int IssueId { get; set; }
        // Object of the issue.
        public Issue Issue { get; set; }
        // Id of the employee.
        public int WorkerId { get; set; }
        // Object of the employee.
        public User Worker { get; set; }

        private static readonly TimeSpan StandardStartTime = TimeSpan.Parse("08:00");
        private static readonly TimeSpan StandardEndTime = TimeSpan.Parse("16:00");

        // Property containeing calculated work time.
        [Display(Name ="Czas pracy")]
        public TimeSpan WorkTime
        {
            get
            {
                return EndTime - StartTime;
            }
        }
        
        // Property containing calculeted overtime hours.       
        [Display(Name = "Nadgodziny")]
        public TimeSpan OverTime
        {
            get
            {
                TimeSpan result = TimeSpan.Zero;
                try
                {
                    if (StartTime.TimeOfDay < StandardStartTime)
                        result += EndTime.TimeOfDay < StandardStartTime && EndTime.TimeOfDay != TimeSpan.Zero ? EndTime.Subtract(StartTime) : StandardStartTime.Subtract(StartTime.TimeOfDay);
                    if (EndTime.TimeOfDay > StandardEndTime || EndTime.TimeOfDay == TimeSpan.Zero)
                        result += StartTime.TimeOfDay > StandardEndTime ? EndTime.Subtract(StartTime) : EndTime.Subtract(StandardEndTime).TimeOfDay;
                    return result;
                }
                catch
                {
                    return TimeSpan.Zero;
                }
            }
        }

        // Method to set notification as archive
        public void Archive()
        {
            ArchivedTimeStamp = DateTime.Now;
        }
    }
}