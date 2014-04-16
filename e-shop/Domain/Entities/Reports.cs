using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;

namespace Domain.Entities
{
    public class Reports
    {
        private ISalesOrderDetail _salesOrderDetail;
        private ISalesOrderHeader _salesOrderHeader;

        public Reports(ISalesOrderDetail salesOrderDetail, ISalesOrderHeader salesOrderHeader)
        {
            _salesOrderDetail = salesOrderDetail;
            _salesOrderHeader = salesOrderHeader;
        }

        public List<ReportProductModel> GetProductReport(int productId)
        {
            var result = (from sales in _salesOrderDetail.SalesOrderDetails where sales.ProductID == productId select new ReportProductModel { Amount = sales.LineTotal, DateTime = sales.ModifiedDate, NumberOsSales = sales.OrderQty }).ToList();
            return result;
        }

        public List<ReportUserModel> GetUserReport(int userId)
        {
            var result = new List<ReportUserModel>();
            foreach (var sales in _salesOrderHeader.SalesOrderHeaders)
            {
                if (sales.CustomerID == userId)
                {
                    result.Add(new ReportUserModel
                    {
                        OrderDate = sales.OrderDate,
                        SubTotal = sales.SubTotal,
                        Address = sales.Address1,
                        ShipMethod = sales.ShipMethod,
                        Status = sales.Status,
                        Products = new List<ReportProductModel>(from s in sales.SalesOrderDetail
                                                                select new ReportProductModel
                                                                    {
                                                                        Amount = s.LineTotal,
                                                                        DateTime = s.ModifiedDate,
                                                                        NumberOsSales = s.OrderQty,
                                                                        Name = s.Product.Name
                                                                    }).ToList()
                    });
                }
            }
            return result;

        }

        public List<ReportProductModel> GetStatisticsBySales()
        {
            var result = new List<ReportProductModel>();
            result = (from sales in _salesOrderDetail.SalesOrderDetails  select new ReportProductModel { Amount = sales.LineTotal, DateTime = sales.ModifiedDate, NumberOsSales = sales.OrderQty, Name = sales.Product.Name}).ToList();
            return result;
        }

    }
}
