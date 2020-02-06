using System;
using System.Collections.Generic;

namespace CoffeeShop.Models
{
    public partial class Items
    {
        public Items()
        {
            UserItemId = new HashSet<UserItemId>();
        }

        public int ItemId { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? WholesalePrice { get; set; }
        public decimal? CustomerDiscountPrice { get; set; }

        public virtual ICollection<UserItemId> UserItemId { get; set; }
    }
}
