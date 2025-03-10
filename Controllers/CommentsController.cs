using Microsoft.AspNetCore.Mvc;
using GalaxyForum.Data;
using GalaxyForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GalaxyForum.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly GalaxyForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(GalaxyForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Comments/Create
        public IActionResult Create(int discussionId)
        {
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
                comment.ApplicationUserId = _userManager.GetUserId(User);
                comment.CreateDate = DateTime.Now;
                _context.Add(comment);
                await _context.SaveChangesAsync();

                // Redirect to the Details action of the DiscussionsController
                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }
            return View(comment);
        }
    }
}