using System.ComponentModel.DataAnnotations;
using Todo_App.Models;

namespace Todo_App.Custom_Data_Annotation
{
    public class ValidUserIdAttribute : ValidationAttribute
    {
     
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value == null)
            {
                return new ValidationResult($"Userid is Required.");
            }
            var context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            var id =Convert.ToInt32(value);
            if (context.Users.Any(u => u.Id == id))
            {
                
                 return ValidationResult.Success;
            }
            return new ValidationResult($"The user is not exist having id {id}.");
        }
    }
}
