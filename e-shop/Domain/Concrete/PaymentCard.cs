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
       
       public void Payment(int orderId, CardParameters param)
       {
           ISalesOrderHeader salesOrderHeader=new EFSalesOrderHeader();
           salesOrderHeader.SalesOrderHeaders.FirstOrDefault(x => x.SalesOrderID == orderId).Status = 1;
           
       }
    }
}
