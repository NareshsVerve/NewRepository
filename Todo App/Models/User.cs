using System.ComponentModel.DataAnnotations;
using Todo_App.Custom_Data_Annotation;

namespace Todo_App.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Value Required.")]
        [StringLength(50,ErrorMessage = "The number of characters must be less than 50.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Alphabets are allowed.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Username Value Required.")]
        [StringLength(50, ErrorMessage = "The number of characters must be less than 50.")]
        [UniqueUsername]
        [ValidateUsername]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [PasswordStrength]
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateDate { get; private set; } 
        public DateTime UpdateDate { get; set; } 

        public virtual ICollection<Todo> TodoList { get; set; } = new List<Todo>();
    }
}
