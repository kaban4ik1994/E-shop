using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
   public interface IProductCategoryRepository
    {
        IQueryable<ProductCategory> ProductCategories { get; }
        void SaveToProductCategory(ProductCategory productCategory);
    }
}
