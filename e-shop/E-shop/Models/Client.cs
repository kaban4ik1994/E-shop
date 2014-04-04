using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_shop.Models
{
    public class Client : User
    {
      public Basket Basket { get; set; }
    }
}