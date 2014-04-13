using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IAddressRepository
    {
        IQueryable<Address> Address{ get; }
        void SaveToAddress(Address address);
    }
}
