using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models.Models
{
    public class Product
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [DisplayName("List Price")]
        [Range(1,1000)]
        public double ListPrice { get; set; }
        [Required]
        [DisplayName("Price 1-50")]
        [Range(1,1000)]
        public double Price { get; set; }
        [Required]
        [DisplayName("Price 50+" )]
        [Range(1,1000)]
        public double Price50 { get; set; }
        [Required]
        [DisplayName("Price 100+")]
        [Range(1,1000)]
        public double Price100 { get; set; }
        [Required]
        public int? CategoryID {  get; set; }
        public string? ImageUrl { get; set; }
        //nevigation property
        public Category? Category { get; set; }

    }
}
