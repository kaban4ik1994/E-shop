﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;

namespace Domain.Concrete
{
    public class EfAddressRepository : IAddressRepository
    {
        private AdventureWorksLT2012_DataEntities _context = new AdventureWorksLT2012_DataEntities();
        public IQueryable<Address> Address
        {
            get
            {
               return _context.Address;
            }
        }
        public void SaveToAddress(Address address)
        {
            using (var db = new AdventureWorksLT2012_DataEntities())
            {
               
                db.Entry(address).State = address.AddressID == 0 ? EntityState.Added : EntityState.Modified;

                if (address.AddressID == 0)
                {
                    db.Address.Add(address);
                }
                db.SaveChanges();

            }
        }
    }
}