
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ReportUserModel
    {
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public List<ReportProductModel> Products { get; set; }
        public Address Address { get; set; }
        public string ShipMethod { get; set; }
        public int Status { get; set; }
    }
}
