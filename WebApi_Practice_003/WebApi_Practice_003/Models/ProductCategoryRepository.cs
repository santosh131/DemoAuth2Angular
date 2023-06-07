using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Practice_003.Models
{
    public class ProductCategoryRepository:IProductCategoryRepository
    {
        TestContext tc = new TestContext();

        public IEnumerable<ProductCategory> GetAll()
        {
            return tc.ProductCategorys.ToList();
        }
    }
}