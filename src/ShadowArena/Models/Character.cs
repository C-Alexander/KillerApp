using System.Collections.Generic;

namespace Shadow_Arena.Models
{
    public partial class Character
    {
        public Character()
        {
            CharacterItem = new HashSet<CharacterItem>();
            CharacterSkill = new HashSet<CharacterSkill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? OwningPlayerid { get; set; }
        public int Classid { get; set; }
        public int Statid { get; set; }

        public virtual ICollection<CharacterItem> CharacterItem { get; set; }
        public virtual ICollection<CharacterSkill> CharacterSkill { get; set; }
        public virtual Class Class { get; set; }
        public virtual Player OwningPlayer { get; set; }
        public virtual Stat Stat { get; set; }
    }
}
