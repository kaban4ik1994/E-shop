using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
                _context.Customer.Include(x => x.CustomerAddress);
                _context.CustomerAddress.Include(x => x.Address);
              
                   return _context.Customer;
            }
        }

        public void SaveToUser(Customer user)
        {
            using (var db = new AdventureWorksLT2012_DataEntities())
            {
                var a = db.GetValidationErrors();
                db.Entry(user).State = user.CustomerID == 0 ? EntityState.Added : EntityState.Modified;
                db.Customer.Include(x => x.CustomerAddress);
                db.CustomerAddress.Include(x => x.Address);
               
                if (user.CustomerID == 0)
                    db.Customer.Add(user);
                db.SaveChanges();
               
                
            }

        }

        public Customer GetCustomerByUniqueidentifier(string uniqueidentifier)
        {
            using (var db = new AdventureWorksLT2012_DataEntities())
            {
                return db.Customer.FirstOrDefault(u => u.rowguid.ToString() == uniqueidentifier);

            }

        }
    }
}
