using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public class Issue
    {
        public int Id { get; set; }
        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Wymagane jest podanie tytułu.")]
        public string Title { get; set; }
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Wymagane jest podanie opisu.")]
        public string Description { get; set; }
        public DateTime? ArchivedTimeStamp { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int AgentId { get; set; }
        public User Agent { get; set; }
        public int NotifierId { get; set; }
        public User Notifier { get; set; }
        public int Companyid { get; set; }
        public Company Company { get; set; }

        public List<Notification> Notifications { get; set; }

        public void Archive(Status status)
        {
            Status = status;
            foreach (var Notification in Notifications)
                Notification.Archive();
            ArchivedTimeStamp = DateTime.Now;
        }
    }
}