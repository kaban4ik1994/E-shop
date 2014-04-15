using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;

namespace Domain.Concrete
{
   public class EFSalesOrderDetails: ISalesOrderDetail
    {
       private AdventureWorksLT2012_DataEntities _context=new AdventureWorksLT2012_DataEntities();

       public IQueryable<SalesOrderDetail> SalesOrderDetails
       {
           get
           {
               return _context.SalesOrderDetail;
           }
       }
       public void AddSalesOrderDetails(SalesOrderDetail salesOrderDetail)
       {
           throw new NotImplementedException();
       }
    }
}
