using System.Collections.Generic;

namespace Shadow_Arena.Models
{
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class Shop
    {
        public Shop()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Item = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float PriceModifier { get; set; }

        public virtual ICollection<Item> Item { get; set; }
    }
}
