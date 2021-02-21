using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cart.Models
{
    public class User
    {
        public string account { set; get; }
        public string password1 { set; get; }
        public string password2 { set; get; }
        public int city { set; get; }
        public string villiage { set; get; } 
    }
}