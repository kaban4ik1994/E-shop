using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
   public interface IPayment<in T>
   {
       void Payment(int orderId, T param);
   }
}
