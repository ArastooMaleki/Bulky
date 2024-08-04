using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.Models
{
    public class ShoppingCart
    {
        public Guid ID { get; set; }
        public int ProductID { get; set; }
        public string ApplicationUserID { get; set; }
        [Range(1,1000,ErrorMessage ="please insert a number between 1 and 1000")]
        public int Count { get; set; }
        //navigation property 
        [ForeignKey("ProductID")]
        [ValidateNever]
        public Product Product { get; set; } = null!;
        [ForeignKey("ApplicationUserID")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
