using System;
using System.Collections.Generic;

namespace PRSProjectV1.Models
{
    public partial class Product
    {
        public Product()
        {
            RequestLine = new HashSet<RequestLine>();
        }

        public int Id { get; set; }
        public string PartNbr { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string PhotoPath { get; set; }
        public int? VendorId { get; set; }

        public virtual Vendors Vendor { get; set; }
        public virtual ICollection<RequestLine> RequestLine { get; set; }
    }
}
