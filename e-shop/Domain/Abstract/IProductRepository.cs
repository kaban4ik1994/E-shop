using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void SaveToProduct(Product product);

        void DeleteProduct(Product product);

    }
}
