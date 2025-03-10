using GalaxyForum.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyForum.Models
{
    public class Discussion
    {
        public int DiscussionId { get; set; }  
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImageFilename { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;

        public ApplicationUser? ApplicationUser { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }
    }
}