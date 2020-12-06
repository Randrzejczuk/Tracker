using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int AgentId { get; set; }
        public User Agent { get; set; }
        public int NotifierId { get; set; }
        public User Notifier { get; set; }
        public int Companyid { get; set; }
        public Company Company { get; set; }

        public List<Notification> Notifications { get; set; }
    }
}