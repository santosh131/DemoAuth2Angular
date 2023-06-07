using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Practice_003.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Byte[] Bytes { get; set; }

    }
}