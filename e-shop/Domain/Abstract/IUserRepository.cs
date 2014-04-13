using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
       IQueryable<Customer> Users { get; }

        void SaveToUser(Customer user);

        Customer GetCustomerByUniqueidentifier(string uniqueidentifier);


    }
}
