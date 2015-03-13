using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
   public class EFProductDescription: IProductDescription
    {
       private AdventureWorksLT2012_DataEntities _context = new AdventureWorksLT2012_DataEntities();

       public IQueryable<ProductDescription> ProductDescriptions
       {
           get
           {
               return _context.ProductDescription;
           }
       }
       public void SaveToProductDescription(ProductDescription productDescription)
       {
           
           _context.Entry(productDescription).State = (EntityState) (productDescription.ProductDescriptionID == 0 ? EntityState.Added : EntityState.Modified);
           _context.SaveChanges();
       }
    }
}
