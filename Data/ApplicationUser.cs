using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyForum.Data
{
   public class ApplicationUser : IdentityUser
   {
        [PersonalData]
        public string Name { get; set; }
        //TODO enforce uniqueness next update

        [PersonalData]
        public string? Location { get; set; } = string.Empty;

        [PersonalData]
        public string? ImageFilename { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile? Image { get; set; }
}
}