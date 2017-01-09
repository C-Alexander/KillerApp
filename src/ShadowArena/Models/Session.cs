using System;
using System.Collections.Generic;

namespace ShadowArena.Models
{
    public partial class Session
    {

        public string Id { get; set; }
        public string UserAgent { get; set; }
        public string Ip { get; set; }
    }
}
