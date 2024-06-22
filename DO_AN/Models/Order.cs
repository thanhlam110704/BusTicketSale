using System;
using System.Collections.Generic;

namespace DO_AN.Models
{
    public partial class Order
    {
        public Order()
        {
            Customers = new HashSet<Customer>();
            OrderTickets = new HashSet<OrderTicket>();
        }

        public int IdOrder { get; set; }
        public double UnitPrice { get; set; }
        public DateTime DateOrder { get; set; }
        public int IdTicket { get; set; }
        public int IdDiscount { get; set; }

        public virtual Discount IdDiscountNavigation { get; set; } = null!;
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<OrderTicket> OrderTickets { get; set; }
    }
}
