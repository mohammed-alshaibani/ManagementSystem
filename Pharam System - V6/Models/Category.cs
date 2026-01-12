using System.ComponentModel.DataAnnotations;

namespace Pharam_System___V6.Models
{
    public class Category
    {
        [Key]
        [Required]
        [Display(Name ="رقم الصنف")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "إسم الصنف")]
        public string CategoryName { get; set; }
    }
}
