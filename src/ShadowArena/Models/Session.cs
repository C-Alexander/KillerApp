using System;
using System.Collections.Generic;

namespace ShadowArena.Models
{
    public partial class Session
    {
        public Session()
        {
            PlayerSession = new HashSet<PlayerSession>();
        }

        public string Id { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }

        public virtual ICollection<PlayerSession> PlayerSession { get; set; }
    }
}
