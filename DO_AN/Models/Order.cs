﻿using System;
using System.Collections.Generic;

namespace DO_AN.Models
{
    public partial class Order
    {
        public Order()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int IdOrder { get; set; }
        public double? UnitPrice { get; set; }
        public DateTime? DateOrder { get; set; }
        public int? IdDiscount { get; set; }
        public string? NameCus { get; set; }
        public string? Phone { get; set; }
        public int? IdCus { get; set; }

        public virtual Customer? IdCusNavigation { get; set; }
        public virtual Discount? IdDiscountNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
