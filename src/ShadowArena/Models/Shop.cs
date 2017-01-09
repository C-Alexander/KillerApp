using System;
using System.Collections.Generic;

namespace ShadowArena.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Item = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float PriceModifier { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
