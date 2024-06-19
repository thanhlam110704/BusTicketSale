using System;
using System.Collections.Generic;

namespace DO_AN.Models
{
    public partial class Customer
    {
        public int IdCus { get; set; }
        public string FullName { get; set; } = null!;
        public int? IdAccount { get; set; }
        public int? IdOrder { get; set; }

        public virtual Account? IdAccountNavigation { get; set; }
        public virtual Order? IdOrderNavigation { get; set; }
    }
}
