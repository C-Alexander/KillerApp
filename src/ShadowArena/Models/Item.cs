using System.Collections.Generic;

namespace ShadowArena.Models
{
    public partial class Item
    {
        public Item()
        {
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
