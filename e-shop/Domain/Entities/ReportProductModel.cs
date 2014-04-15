using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ReportProductModel
    {
        public string Name { get; set; }
        public int NumberOsSales { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; } 
    }
}
