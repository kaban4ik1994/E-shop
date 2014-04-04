using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_shop.Models
{
    public class Basket
    {
        public List<Order> Orders { get; set; }//множество заказов

    }
}