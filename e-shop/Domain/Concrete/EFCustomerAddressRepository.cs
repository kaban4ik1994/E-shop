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

            var customerAdressInDb =
                _context.CustomerAddress.FirstOrDefault(
                    x => x.AddressID == customerAddress.AddressID && x.CustomerID == customerAddress.CustomerID);
            if (customerAdressInDb == null)
            {
                _context.Entry(customerAddress).State = (EntityState)System.Data.EntityState.Added;
            }

            _context.Entry(customerAddress.Address).State = EntityState.Unchanged;
            _context.Entry(customerAddress.Customer).State = EntityState.Unchanged;
            _context.SaveChanges();
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
