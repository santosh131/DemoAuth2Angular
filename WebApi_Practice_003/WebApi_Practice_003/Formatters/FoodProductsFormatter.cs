using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using WebApi_Practice_003.Models;

namespace WebApi_Practice_003.Formatters
{
    public class FoodProductsFormatter : BufferedMediaTypeFormatter
    {
        public FoodProductsFormatter()
        {
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("application/custom_food_products"));
            SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/custom_food_products"));
            SupportedEncodings.Add(new UTF8Encoding(false, true));
            SupportedEncodings.Add(new UnicodeEncoding(false, true, true));
        }

        public override bool CanReadType(Type type)
        {
            if (type == typeof(FoodProductsModel))
                return true;
            else if (type == typeof(IEnumerable<FoodProductsModel>))
                return true;
            else
                return false;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            Encoding effectiveEncoding = SelectCharacterEncoding(content.Headers);
            using (StreamWriter sWriter = new StreamWriter(writeStream, effectiveEncoding))
            {
                if (value is FoodProductsModel)
                {
                    var str = SerializeResourceToMyType((FoodProductsModel)value);
                    sWriter.Write(str);
                }
                else if (value is IEnumerable<FoodProductsModel>)
                {
                    var str = SerializeResourceToMyType((FoodProductsModel)value);
                    sWriter.Write(str);
                }
            }
        }
        private string SerializeResourceToMyType(FoodProductsModel fpm)
        {
            return "The food product named " + fpm.ProductName + " has a price of " + fpm.Price;
        }

        private string SerializeResourceToMyType(IEnumerable<FoodProductsModel> lstFpm)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var fpm in lstFpm)
            {
                sb.Append("The food product named " + fpm.ProductName + " has a price of " + fpm.Price);
            }

            return sb.ToString();          
        }


        public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
        {
            FoodProductsModel fpm = new FoodProductsModel();
            StreamReader reader = new StreamReader(readStream);
            string str = reader.ReadToEnd();
            String[] propArr = str.Split(new char[] { '|' });
            foreach (var val in propArr)
            {
                string[] propIntArr = val.Split(new char[] { '=' });
                //if (propIntArr[0].ToLower().Trim().Equals("CategoryId"))
                //{
                //    fpm.CategoryId =int.Parse(propIntArr[1].ToString());
                //}
                //else if (propIntArr[0].ToLower().Trim().Equals("position"))
                //{
                //    fpm.Position = propIntArr[1].ToString();
                //}

                SetProp(propIntArr[0].ToLower().Trim(), propIntArr[1].ToLower().Trim(), ref fpm);
            }

            return fpm;
        }

        private void SetProp(string propName, string value, ref FoodProductsModel fpm)
        {
            PropertyInfo pInfo = fpm.GetType().GetProperty(propName);
            if (pInfo != null)
            {
                if (pInfo.GetType() == typeof(int))
                {
                    int i = 0;
                    int.TryParse(value, out i);
                    pInfo.SetValue(fpm, i, null);
                }
                else if (pInfo.GetType() == typeof(decimal))
                {
                    decimal i = 0;
                    decimal.TryParse(value, out i);
                    pInfo.SetValue(fpm, i, null);
                }
                else if (pInfo.GetType() == typeof(DateTime))
                {
                    DateTime dt =DateTime.Now;
                    DateTime.TryParse(value, out dt);
                    pInfo.SetValue(fpm, dt, null);
                }
                else
                {
                    pInfo.SetValue(fpm, value, null);
                }
            }
        }

    }
}