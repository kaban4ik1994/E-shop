using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;

namespace WebUI.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public ProductDescription ProductDescription { get; set; }
    }
}