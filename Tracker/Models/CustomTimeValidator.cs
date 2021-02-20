using System;
using System.ComponentModel.DataAnnotations;    
using Tracker.ViewModels;

namespace Tracker.Models
{
    public class CustomTimeValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var notification = (CreateNotificationViewModel)validationContext.ObjectInstance;
            TimeSpan StartTime = TimeSpan.Parse(notification.StartTime);
            TimeSpan EndTime = TimeSpan.Parse(notification.EndTime);
            if (EndTime <= StartTime && EndTime != TimeSpan.Zero)
                return new ValidationResult("Czas zakończenia pracy musi być większy niż czas rozpoczęcia");
            return ValidationResult.Success;
        }
    }
}