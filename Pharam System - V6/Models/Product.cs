using System.ComponentModel.DataAnnotations;

namespace Pharam_System___V6.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "إسم المنتج")]
        public string ProductName { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "السعر")]
        public decimal ProductPrice { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "وصف المنتج")]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name ="الصورة")]
        public string ProductImage { get; set; }
    }
}
