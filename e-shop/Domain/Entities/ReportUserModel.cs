
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class ReportUserModel
    {
        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public List<ReportProductModel> Products { get; set; }

    }
}
