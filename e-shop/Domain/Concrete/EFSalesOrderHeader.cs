using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;

namespace Domain.Concrete
{
   public class EFSalesOrderHeader: ISalesOrderHeader
    {
       private AdventureWorksLT2012_DataEntities _context=new AdventureWorksLT2012_DataEntities();

       public IQueryable<SalesOrderHeader> SalesOrderHeaders
       {
           get
           {
               return _context.SalesOrderHeader;
           }
       }
       public void AddSalesOrderHeader(SalesOrderHeader salesOrderHeader)
       {
          _context.Entry(salesOrderHeader).State=EntityState.Added;
           _context.SaveChanges();
       }
    }
}
