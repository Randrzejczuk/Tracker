using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tracker.Models;

namespace Tracker.ViewModels
{
    public class CreateNotificationViewModel
    {
        public Notification Notification { get; set; }
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        [Display(Name = "Czas rozpoczęcia Pracy")]
        public string StartTime { get; set; }
        [Display(Name = "Czas Zakończenia Pracy")]
        public string EndTime { get; set; }
        [Display(Name = "Pracownik")]
        public List<User> Workers { get; set; }

        public List<string> Times
        {
            get
            {
                List<string> results = new List<string>();
                string element;
                for (int i = 0; i < 24; i++)
                {
                    for (int j = 0; j < 60; j += 15)
                    {
                        if (i < 10)
                            element = j == 0 ? $"0{i}:00" : $"0{i}:{j}";
                        else
                            element = j == 0 ? $"{i}:00" : $"{i}:{j}";
                        results.Add(element);
                    }
                }
                return (results);
            }
        }
    }
}