using System.ComponentModel.DataAnnotations;
using Todo_App.Models;

namespace Todo_App.Custom_Data_Annotation
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            if(value == null)
            {
                return new ValidationResult($"Username is Required.");
            }
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var username = value as string;
            if (context.Users.Any(u => u.UserName == username))
            {
               
                return new ValidationResult($"Username is already in use.");
            }
            return ValidationResult.Success;
        }
    }
}
