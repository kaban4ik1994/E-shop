﻿using System.Linq;
using Domain.Entities;

namespace Domain.Abstract
{
   public interface IProductRepository
    {
       IQueryable<Product> Products { get; } 
    }
}