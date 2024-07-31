using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Todo_App.Custom_Data_Annotation
{
    public class PasswordStrengthAttribute : ValidationAttribute
    {
        public PasswordStrengthAttribute()
        {
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character (@,#,$,&,*) and be at least 8 characters long.";
        }
        protected override ValidationResult IsValid(object value, 
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"Password is Required.");
            }
            var password = value.ToString();
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$&*])[A-Za-z\d@#$&*]{8,}$");
            if (!regex.IsMatch(password))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
