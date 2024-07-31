using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Todo_App.Custom_Data_Annotation
{
    public class ValidateUsernameAttribute: ValidationAttribute
    {
        public ValidateUsernameAttribute()
        {
            ErrorMessage = "Username must contain at least one uppercase letter, one lowercase letter, one number, one special character (@,#,$,&,*) and be at least 6 characters long.";
        }
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"Username is Required.");
            }
            var username = value.ToString();
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$&*])[A-Za-z\d@#$&*]{6,}$");
            if (!regex.IsMatch(username))
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
