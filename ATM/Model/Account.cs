using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATM.Model
{
    public class Account
    {
        public decimal cardNumber { get; set; }
        public int pin { get; set; }
        public int amount { get; set; }
        public char accountType { get; set; }
        public DateTime modifiedDate { get; set; }
    }
}
