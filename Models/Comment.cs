using GalaxyForum.Data;

namespace GalaxyForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }  // Primary key
        public string Content { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public int DiscussionId { get; set; }
        public Discussion? Discussion { get; set; }

        //Foreign key(AspNetUsers table)
        public string ApplicationUserId { get; set; } = string.Empty;
        // Navigation property
        public ApplicationUser? ApplicationUser { get; set; } = null; // nullable!!!
    }
}
