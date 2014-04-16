using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;

namespace Domain.Concrete
{
   public class EFProductModelProductDescription: IProductModelProductDescription
    {
       private AdventureWorksLT2012_DataEntities _context = new AdventureWorksLT2012_DataEntities();

       public IQueryable<ProductModelProductDescription> ProductModelProductDescriptions
       {
           get
           {
               return _context.ProductModelProductDescription;
           }
       }

       public void SaveToProductModelProductDescription(ProductModelProductDescription productModelProductDescription)
       {
        
           _context.Entry(ProductModelProductDescriptions).State = EntityState.Unchanged;
        
           _context.SaveChanges(); 
       }
    }
}
