using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_shop.Models
{
    public class Order 
    {
        public Product Product { get; set; }//конкретный товар
        public int Number { get; set; }//количество 
    }
}