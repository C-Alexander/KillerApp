using System;

namespace Shadow_Arena.Models
{
    // ReSharper disable once PartialTypeWithSinglePart
    public partial class PlayerSession
    {
        public int Playerid { get; set; }
        public string Sessionid { get; set; }
        public DateTime? LoginTime { get; set; }

        public virtual Player Player { get; set; }
        public virtual Session Session { get; set; }
    }
}
