using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_Practice_003.Models
{
    interface IProductCategoryRepository
    {
        IEnumerable<ProductCategory> GetAll();
    }
}
