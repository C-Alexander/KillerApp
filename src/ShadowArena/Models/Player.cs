using System;
using System.Collections.Generic;

namespace ShadowArena.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }

        public virtual ICollection<Character> Character { get; set; }
        public virtual ICollection<PlayerSession> PlayerSession { get; set; }
    }
}
