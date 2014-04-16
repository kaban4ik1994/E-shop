﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IOrderProcessor
    {
        void Processor(Cart cart,  Customer user, Address addresss, string shipMethod);
    }
}