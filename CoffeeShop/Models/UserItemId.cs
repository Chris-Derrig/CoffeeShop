using System;
using System.Collections.Generic;

namespace CoffeeShop.Models
{
    public partial class UserItemId
    {
        public int UserItemId1 { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }

        public virtual Items Item { get; set; }
        public virtual User User { get; set; }
    }
}
