using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using System.Data.Entity;
using System.Data.Objects;

namespace Domain.Concrete
{
    public class EfProductRepository : IProductRepository
    {

        private AdventureWorksLT2012_DataEntities _context = new AdventureWorksLT2012_DataEntities();

        public IQueryable<Product> Products
        {
            get
            {
                _context = new AdventureWorksLT2012_DataEntities();
                return _context.Product;
            }

        }


        public void SaveToProduct(Product product)
        {

            using (var db = new AdventureWorksLT2012_DataEntities())
            {
                if (product.ProductID == 0)
                    db.Product.Add(product);

                db.Entry(product).State = product.ProductID == 0 ? EntityState.Added : EntityState.Modified;

                db.SaveChanges();
            }
        }



        public void DeleteProduct(Product product)
        {
            using (var db = new AdventureWorksLT2012_DataEntities())
            {
                db.Entry(product).State = EntityState.Deleted;

                _context.Product.Remove(product);
                _context.SaveChanges();
            }

        }
    }
}
