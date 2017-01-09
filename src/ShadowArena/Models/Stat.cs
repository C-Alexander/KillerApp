using System;
using System.Collections.Generic;

namespace ShadowArena.Models
{
    public partial class Stat
    {
        public Stat()
        {
            Character = new HashSet<Character>();
            Class = new HashSet<Class>();
            Item = new HashSet<Item>();
        }

        public int Id { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int MagicAttack { get; set; }
        public int MagicDefence { get; set; }
        public int Vitality { get; set; }
        public int Intelligence { get; set; }

        public virtual ICollection<Character> Character { get; set; }
        public virtual ICollection<Class> Class { get; set; }
        public virtual ICollection<Item> Item { get; set; }
    }
}
