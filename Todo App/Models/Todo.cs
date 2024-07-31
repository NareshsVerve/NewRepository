using System.ComponentModel.DataAnnotations;
using Todo_App.Custom_Data_Annotation;

namespace Todo_App.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Alphabets are allowed.")]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; }
       
        public TodoStatus Status { get; set; }
        [ValidUserId]
        public int UserId {  get; set; }

        public virtual User? User { get; set; }

    }
}
