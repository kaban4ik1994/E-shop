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
   public class EFProductCategoryRepository: IProductCategoryRepository
    {

       private AdventureWorksLT2012_DataEntities _context=new AdventureWorksLT2012_DataEntities();

       public IQueryable<ProductCategory> ProductCategories
       {
           get
           {
             return  _context.ProductCategory;
           }
       }
       public void SaveToProductCategory(ProductCategory productCategory)
       {
         
           
               _context.Entry(productCategory).State = (EntityState) (productCategory.ProductCategoryID == 0 ? EntityState.Added : EntityState.Modified);
               _context.SaveChanges();
              
       }
    }
}
