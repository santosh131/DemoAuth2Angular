using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace WebApi_Practice_003.Models
{
    public class FoodRepository : IFoodRepository
    {
        TestContext tc = new TestContext();  

        public List<FoodProductsModel> GetAll()
        {
            return tc.FoodProducts.ToList();
        }

        public FoodProductsModel Get(int id)
        {
            return tc.FoodProducts.Find(id);
        }

        public IEnumerable<FoodProductsModel> SearchFor(Expression<Func<FoodProductsModel, bool>> predicate)
        {
            return tc.FoodProducts.Where(predicate);
        }

        public FoodProductsModel Add(FoodProductsModel fpm)
        {
            if (fpm == null)
            {
                throw new ArgumentNullException("FoodProductsModel");
            }
            IEnumerable<FoodProductsModel> fpmAll = GetAll();
            int maxId = fpmAll.Max(f => f.Id);
            fpm.Id = maxId + 1;
            FoodProductsModel newFPM= tc.FoodProducts.Add(fpm);
            tc.SaveChanges();
            return newFPM;
        } 

        public void Update(FoodProductsModel fpm)
        {
            if (fpm == null)
            {
                throw new ArgumentNullException("FoodProductsModel");
            } 
            tc.Entry(fpm).State = System.Data.Entity.EntityState.Modified;
            tc.SaveChanges();
        } 

        public void Remove(int id)
        {
            FoodProductsModel fpm = Get(id);
            if (tc.FoodProducts.Contains(fpm))
            {
                tc.FoodProducts.Remove(fpm);
                tc.SaveChanges();
            }
        }

    }
}