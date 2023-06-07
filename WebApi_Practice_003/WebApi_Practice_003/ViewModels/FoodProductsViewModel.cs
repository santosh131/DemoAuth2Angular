using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi_Practice_003.Models;

namespace WebApi_Practice_003.ViewModels
{
    public class FoodProductsViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public Decimal Price { get; set; }

        public int Quantity { get; set; }

        public string ImageFileName { get; set; }

        public FoodProductsViewModel()
        {
        }
        public FoodProductsViewModel(FoodProductsModel fpm)
        {
            this.Id = fpm.Id;
            this.ProductName = fpm.ProductName;
            this.CategoryId = fpm.CategoryId;
            this.Quantity = fpm.Quantity;
            this.Price = fpm.Price;
            this.ImageFileName = fpm.ImageFileName;
            if (fpm.ProductCategory != null)
                this.CategoryName = fpm.ProductCategory.CategoryName;
        }
    }
}