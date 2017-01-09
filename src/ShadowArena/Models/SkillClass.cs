namespace Shadow_Arena.Models
{
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class SkillClass
    {
        public int Skillid { get; set; }
        public int Classid { get; set; }

        public virtual Class Class { get; set; }
        public virtual Skill Skill { get; set; }
    }
}
