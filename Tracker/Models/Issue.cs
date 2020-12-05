using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int AgentId { get; set; }
        public int NotifierId { get; set; }
        public int Companyid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}