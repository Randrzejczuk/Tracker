using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string WorkDone { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int IssueId { get; set; }
        public Issue Issue { get; set; }
        public int WorkerId { get; set; }
        public User Worker { get; set; }
    }
}