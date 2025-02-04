using System.ComponentModel.DataAnnotations;

namespace Basic_Appointment_Management.Validators
{
    public class DateTimeChecker : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var data = (DateTime?)value;

            if (data>DateTime.Now)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("DateTime must be in future");
        }
    }
}
