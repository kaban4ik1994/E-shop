using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;

namespace Domain.Concrete
{
    public class EFProductModelRepository : IProductModelRepository
    {
        private AdventureWorksLT2012_DataEntities _context = new AdventureWorksLT2012_DataEntities();
        
        public IQueryable<ProductModel> ProductModels
        {
            get
            {
                return _context.ProductModel;
            }
        }

        public void SaveToProductModel(ProductModel productModel)
        {
          
      
            _context.Entry(productModel).State = (EntityState) (productModel.ProductModelID == 0 ? System.Data.EntityState.Added : System.Data.EntityState.Modified);
            _context.SaveChanges();

            }
    }
}
