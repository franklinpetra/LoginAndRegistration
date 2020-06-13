using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoginAndRegistration.Models
{
    public class Wrapper
    {
        public List<LRUser> AllUsers { get; set; }
        public List<LRLogin> AllLogins { get; set; }
        public LRUser UserForm { get; set; }
        public LRLogin LoginForm { get; set; }
        public int OneUserId { get; set; }
    }
}

