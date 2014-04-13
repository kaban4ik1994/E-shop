using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFCustomerAddressRepository : IAddressCustomerRepository
    {
        private AdventureWorksLT2012_DataEntities _context = new AdventureWorksLT2012_DataEntities();

        public IQueryable<CustomerAddress> CustomerAddresses
        {
            get
            {
                return _context.CustomerAddress;
            }
        }
        public void SaveToCustomerAddress(CustomerAddress customerAddress)
        {
            using (var db = new AdventureWorksLT2012_DataEntities())
            {
                var customerAdressInDb =
                    db.CustomerAddress.FirstOrDefault(
                        x => x.AddressID == customerAddress.AddressID && x.CustomerID == customerAddress.CustomerID);
                if (customerAdressInDb == null)
                {
                    db.Entry(customerAddress).State = (EntityState)System.Data.EntityState.Added;
                }

                db.Entry(customerAddress.Address).State = EntityState.Unchanged;
                db.Entry(customerAddress.Customer).State = EntityState.Unchanged;
                db.SaveChanges();
            }
        }

        public CustomerAddress BindCustomerAddress(Customer customer, Address address)
        {

            return new CustomerAddress
            {
                Address = address,
                AddressID = address.AddressID,
                AddressType = "",
                Customer = customer,
                CustomerID = customer.CustomerID,
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid()
            };
        }
    }
}
