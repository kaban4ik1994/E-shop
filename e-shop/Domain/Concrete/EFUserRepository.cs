using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;

namespace Domain.Concrete
{
    public class EfUserRepository : IUserRepository
    {
        private AdventureWorksLT2012_DataEntities _context=new AdventureWorksLT2012_DataEntities();

        public IQueryable<Customer> Users
        {
            get
            {
                return _context.Customer;
            }
        }

        public void SaveToUser(Customer user)
        {
            if (user.CustomerID == 0)
                _context.Customer.Add(user);
            _context.Entry(user).State = user.CustomerID == 0 ? EntityState.Added : EntityState.Modified;

            _context.SaveChanges();
        }

        public Customer GetCustomerByUniqueidentifier(string uniqueidentifier)
        {
           return _context.Customer.FirstOrDefault(u => u.rowguid.ToString() == uniqueidentifier);
        }
    }
}
