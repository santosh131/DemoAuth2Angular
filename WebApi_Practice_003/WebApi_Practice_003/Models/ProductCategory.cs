using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApi_Practice_003.Models
{
    public class ProductCategory
    {
        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}