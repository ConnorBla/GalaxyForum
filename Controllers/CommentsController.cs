using Microsoft.AspNetCore.Mvc;
using GalaxyForum.Data;
using GalaxyForum.Models;

namespace GalaxyForum.Controllers
{
    public class CommentsController : Controller
    {
        private readonly GalaxyForumContext _context;

        public CommentsController(GalaxyForumContext context)
        {
            _context = context;
        }

        // GET: Comments/Create
        public IActionResult Create(int discussionId)
        {
            // Pass DiscussionId to the view for hidden field usage
            var comment = new Comment { DiscussionId = discussionId };
            return View(comment);
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreateDate = DateTime.Now;
                comment.Discussion = await _context.Discussions.FindAsync(comment.DiscussionId);
                _context.Add(comment);
                await _context.SaveChangesAsync();

                // Redirect to the Details action of the DiscussionController
                return RedirectToAction("Details", "Discussion" , new { id = comment.DiscussionId });
            }
            return View(comment);
        }
    }
}