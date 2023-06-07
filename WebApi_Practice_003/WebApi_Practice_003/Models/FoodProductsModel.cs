using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi_Practice_003.Models
{
    public class FoodProductsModel
    {
        [Key]
        public int Id { get; set; }

        public string ProductName { get; set; }
        
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }

        public Decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageFileName { get; set; }
    }
}