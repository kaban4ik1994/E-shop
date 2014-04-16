using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
   public interface IProductModelRepository
    {
        IQueryable<ProductModel> ProductModels { get; }
        void SaveToProductModel(ProductModel productModel);
    }
}
