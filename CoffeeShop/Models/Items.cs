using System;
using System.Collections.Generic;

namespace CoffeeShop.Models
{
    public partial class Items
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public double RetailPrice { get; set; }
        public double WholesalePrice { get; set; }
        public double CustomerDiscountPrice { get; set; }
    }
}
