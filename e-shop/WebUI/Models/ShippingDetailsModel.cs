using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Domain.Entities;

namespace WebUI.Models
{
    public class ShippingDetailsModel
    {
        public Cart Cart { get; set; }
        public Address Address { get; set; }
    }
}