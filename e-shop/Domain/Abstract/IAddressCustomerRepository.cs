using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IAddressCustomerRepository
    {
        IQueryable<CustomerAddress> CustomerAddresses { get; }
        void SaveToCustomerAddress(CustomerAddress customerAddress);
        CustomerAddress BindCustomerAddress(Customer customer, Address address);
    }
}
