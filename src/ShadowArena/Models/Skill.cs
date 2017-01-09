using System;
using System.Collections.Generic;

namespace ShadowArena.Models
{
    public partial class Skill
    {
        public Skill()
        {
            CharacterSkill = new HashSet<CharacterSkill>();
            SkillClass = new HashSet<SkillClass>();
        }

        public int Id { get; set; }
        public int? UpgradesFromSkill { get; set; }
        public int ManaCost { get; set; }
        public int HealthCost { get; set; }
        public int CoolDown { get; set; }
        public int WarmUp { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CharacterSkill> CharacterSkill { get; set; }
        public virtual ICollection<SkillClass> SkillClass { get; set; }
        public virtual Skill UpgradesFromSkillNavigation { get; set; }
        public virtual ICollection<Skill> InverseUpgradesFromSkillNavigation { get; set; }
    }
}
