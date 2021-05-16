using System;
using System.Collections.Generic;

namespace Test.Entities {
    public class Order {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public ICollection<Product> Products { get; set; }
        public decimal Amount { get; set; }
        public Customer Customer { get; set; }
    }
}
