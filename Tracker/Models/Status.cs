using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tracker.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Issue> Issues { get; set; }
    }
}