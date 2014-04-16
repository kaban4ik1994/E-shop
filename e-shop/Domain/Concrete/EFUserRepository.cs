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

        private AdventureWorksLT2012_DataEntities _context = new AdventureWorksLT2012_DataEntities();

        public IQueryable<Customer> Users
        {
            get
            {
                return _context.Customer;
            }
        }

        public void SaveToUser(Customer user)
        {
            
             
                _context.Entry(user).State = user.CustomerID == 0 ? EntityState.Added : EntityState.Modified;
             
               _context.SaveChanges();

        }

        public Customer GetCustomerByUniqueidentifier(string uniqueidentifier)
        {
            using (var db = new AdventureWorksLT2012_DataEntities())
            {
                return db.Customer.FirstOrDefault(u => u.rowguid.ToString() == uniqueidentifier);

            }

        }

        public Address GetAddressesByUserId(int userId)
        {
            using (var db = new AdventureWorksLT2012_DataEntities())
            {
                var customerAddress = db.CustomerAddress.FirstOrDefault(x=>x.CustomerID==userId);
                if(customerAddress==null) return new Address();
                var result = db.Address.FirstOrDefault(x => x.AddressID == customerAddress.AddressID);
                
                return result;
            }
          
        }
    }
}
