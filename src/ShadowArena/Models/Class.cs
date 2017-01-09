using System.Collections.Generic;

namespace ShadowArena.Models
{
    public partial class Class
    {
        public Class()
        {
            Character = new HashSet<Character>();
            SkillClass = new HashSet<SkillClass>();
        }

        public int Id { get; set; }
        public int Statid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Character> Character { get; set; }
        public virtual ICollection<SkillClass> SkillClass { get; set; }
        public virtual Stat Stat { get; set; }
    }
}
