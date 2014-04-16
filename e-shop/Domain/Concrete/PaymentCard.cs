using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Parameters;

namespace Domain.Concrete
{
   public class PaymentCard: IPayment<CardParameters>
    {
       
       public void Payment(int orderId, CardParameters param) //тут заглушка, просто поменяем статус в бд на оплачен
       {
           ISalesOrderHeader salesOrderHeader=new EFSalesOrderHeader();
          var order= salesOrderHeader.SalesOrderHeaders.FirstOrDefault(x => x.SalesOrderID == orderId);
           order.Status = 1;
           salesOrderHeader.AddSalesOrderHeader(order);
           
       }
    }
}
