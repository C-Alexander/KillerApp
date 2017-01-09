using System.Collections.Generic;

namespace Shadow_Arena.Models
{
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class Item
    {
        public Item()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            CharacterItem = new HashSet<CharacterItem>();
        }

        public int Id { get; set; }
        public int Price { get; set; }
        public int MinimumLevel { get; set; }
        public int MaximumLevel { get; set; }
        public int EffectingStatid { get; set; }
        public int SoldInShopid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CharacterItem> CharacterItem { get; set; }
        public virtual Stat EffectingStat { get; set; }
        public virtual Shop SoldInShop { get; set; }
    }
}
