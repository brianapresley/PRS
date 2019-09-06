using System;
using System.Collections.Generic;

namespace PRSProjectV1.Models
{
    public partial class Users
    {
        public Users()
        {
            Request = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsReviewer { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Request> Request { get; set; }
    }
}
