using System.Collections.Generic;

namespace Shadow_Arena.Models
{
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class Skill
    {
        public Skill()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            CharacterSkill = new HashSet<CharacterSkill>();
            // ReSharper disable once VirtualMemberCallInConstructor
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
