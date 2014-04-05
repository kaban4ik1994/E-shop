using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
   public class EfProductRepository: IProductRepository
    {
       private EfDbContext _context=new EfDbContext();

       public IQueryable<Product> Products
       {
           get { return _context.Products; }
       }

    }
}
