

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi_Practice_003.Models;
using WebApi_Practice_003.ViewModels;

namespace WebApi_Practice_003.api
{
    [RoutePrefix("api/FoodProducts")]
    [Authorize]
    public class FoodProductsController : ApiController
    {
        FoodRepository fp = new FoodRepository();
        ProductCategoryRepository pc = new ProductCategoryRepository();

        // Read/GET api/<controller>
        [HttpGet]
        public IEnumerable<FoodProductsViewModel> Get()

        {
            return fp.GetAll().Select(e => new FoodProductsViewModel(e));
        }

        // Read/GET api/<controller>/5
        [HttpGet]
        public FoodProductsModel Get(int id)
        {
            return fp.Get(id);
        }

        [HttpGet]
        [Route("SearchFoodProducts/{prodName}")]
        //Uses the FoodProdcutsFormattter 
        //if Accept in the header request is text/custom_food_products or application/custom_food_products
        //else displays data in Json or Xml format by default
        public FoodProductsModel GetSearchFoodProducts(string prodName)
        {
            return fp.SearchFor(p => p.ProductName == prodName).FirstOrDefault();
        }

        [HttpGet]
        [Route("SearchFoodProducts")]
        public IEnumerable<FoodProductsModel> GetSearchFoodProducts2()
        {
            return fp.GetAll();
        }


        [HttpGet]
        [Route("GetCategories")]
        public IEnumerable<ProductCategory> GetCategories()
        {
            return pc.GetAll();
        }

        // Create/POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromBody]FoodProductsModel value)
        {            
            return Ok(fp.Add(value));
        }

        // Update/PUT api/<controller>/5
        [HttpPut]
        public IHttpActionResult Put([FromBody]FoodProductsModel value)
        {
            fp.Update(value);
            return Ok(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void Delete(int id)
        {
            fp.Remove(id);
        }


        // Create/POST api/<controller>
        [HttpPost]
        [Route("UploadImage")]
        public HttpResponseMessage UploadImage(ImageModel value)
        {
            WriteBytes(value);
            FoodProductsModel fpm= fp.Get(value.Id);
            fpm.ImageFileName ="/Images/"+ value.Name.ToLower()+".png";
            fp.Update(fpm);

            HttpResponseMessage msg = Request.CreateResponse(HttpStatusCode.OK);
            msg.Content = new StringContent("File uploaded succesfully", Encoding.Unicode);
            return msg;
        }

        private void WriteBytes(ImageModel value)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/")+"Images";
            using (var img = Image.FromStream(new MemoryStream(value.Bytes)))
            {
                img.Save(filePath+"/"+value.Name.ToLower() + ".png", ImageFormat.Png);
            }
        }
    }
}