using GalaxyForum.Data;
using System.Collections.Generic;

namespace GalaxyForum.Models
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Discussion> Discussions { get; set; } = [];
    }
}