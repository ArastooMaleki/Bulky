using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models.Models

{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="insert a number betwwn 1 to 100")]
        public int DisplayOrder { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30 , ErrorMessage ="above 30 char is forbiden ")]
        public string? Name { get; set; }
    }
}
/* 
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
*/