using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Activation;

namespace Domain.Parameters
{
    public class CardParameters
    {
        [Required(ErrorMessage = "*")]
        [Range(typeof(long), "1000000000000000","9999999999999999", ErrorMessage = "*")]
        public long Number { get; set; }
        [Required(ErrorMessage = "*")]
        [Range(1,12, ErrorMessage = "*")]
        public int ValidityM { get; set; }
        [Required(ErrorMessage = "*")]
        [Range(2000, 2100, ErrorMessage = "*")]
        public int ValidityY { get; set; }
    }
}
