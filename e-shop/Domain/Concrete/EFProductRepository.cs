using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using System.Data.Entity;

namespace Domain.Concrete
{
    public class EfProductRepository : IProductRepository
    {

        private AdventureWorks2012_DataEntities _context = new AdventureWorks2012_DataEntities();


       

        public IQueryable<Product> Products
        {
            get
            {
                return _context.Product;
            }
        }



        public void SaveToProduct(Product product)
        {
            if (product.ProductID == 0) _context.Product.Add(product);
            _context.SaveChanges();
        }
    }
}
