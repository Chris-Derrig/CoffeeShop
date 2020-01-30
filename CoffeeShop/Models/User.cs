using System;
using System.Collections.Generic;

namespace CoffeeShop.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? Birthday { get; set; }
        public string PhoneNumer { get; set; }
    }
}
