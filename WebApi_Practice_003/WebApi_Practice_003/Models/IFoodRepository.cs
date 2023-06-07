using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_Practice_003.Models
{
    interface IFoodRepository 
    {
        List<FoodProductsModel> GetAll();
        IEnumerable<FoodProductsModel> SearchFor(Expression<Func<FoodProductsModel, bool>> predicate);        
        FoodProductsModel Get(int id);
        FoodProductsModel Add(FoodProductsModel fpm);
        void Update(FoodProductsModel fpm);
        void Remove(int id);
    }
}
