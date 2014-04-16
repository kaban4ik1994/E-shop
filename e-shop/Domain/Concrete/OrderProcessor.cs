using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class OrderProcessor : IOrderProcessor
    {
        public void Processor(Cart cart,  Customer user, Address address, string shipMethod)
        {
            ISalesOrderDetail iSalesOrderDetail = new EFSalesOrderDetails();
            ISalesOrderHeader iSalesOrderHeader = new EFSalesOrderHeader();
            var salesOrderHeader=new SalesOrderHeader
            {
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid(),
                CustomerID = user.CustomerID,
                RevisionNumber = 0,
                OrderDate = DateTime.Now,
                DueDate = DateTime.Now,
                ShipMethod = shipMethod,
                TotalDue = 0,
                SubTotal = cart.ComputeTotalValue(),
                ShipToAddressID = address.AddressID,
                BillToAddressID = address.AddressID
            };
            iSalesOrderHeader.AddSalesOrderHeader(salesOrderHeader);
            foreach (var prod in cart.Lines)
            {
                iSalesOrderDetail.AddSalesOrderDetails(new SalesOrderDetail { UnitPrice = prod.Product.StandardCost, ModifiedDate = DateTime.Now, OrderQty = (short)prod.Quantity, ProductID = prod.Product.ProductID, UnitPriceDiscount = 0, rowguid = Guid.NewGuid(), SalesOrderDetailID = 0, SalesOrderID = salesOrderHeader.SalesOrderID });

                }

        }
    }
}
