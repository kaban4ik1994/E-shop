using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{

    public class CardSettings
    {
        [Required(ErrorMessage = "Please enter a card number")]
        public long CardNumber = 0;
        [Required(ErrorMessage = "Please enter a CardExpMonth")]
        public int CardExpMonth = 1;
        [Required(ErrorMessage = "Please enter a CardExpYear")]
        public int CardExpYear = 2000;
    }
    public class CardOrderProcessor : IOrderProcessor
    {
        private CardSettings _cardSettings;

        public CardOrderProcessor(CardSettings settings)
        {
            _cardSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails) //заглушка
        {
    
        }
    }
}
