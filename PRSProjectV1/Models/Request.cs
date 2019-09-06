using System;
using System.Collections.Generic;

namespace PRSProjectV1.Models
{
    public partial class Request
    {
        public Request()
        {
            RequestLine = new HashSet<RequestLine>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public string RejectionReason { get; set; }
        public string DeliveryMode { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<RequestLine> RequestLine { get; set; }
    }
}
