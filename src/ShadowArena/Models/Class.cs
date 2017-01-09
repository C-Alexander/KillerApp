using System.Collections.Generic;

namespace Shadow_Arena.Models
{
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class Class
    {
        public Class()
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Character = new HashSet<Character>();
            // ReSharper disable once VirtualMemberCallInConstructor
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
