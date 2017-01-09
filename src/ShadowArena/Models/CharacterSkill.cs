using System;
using System.Collections.Generic;

namespace ShadowArena.Models
{
    public partial class CharacterSkill
    {
        public int Characterid { get; set; }
        public int Skillid { get; set; }

        public virtual Character Character { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
