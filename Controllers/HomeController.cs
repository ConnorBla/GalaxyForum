using System.Diagnostics;
using GalaxyForum.Data;
using GalaxyForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GalaxyForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly GalaxyForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(GalaxyForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var discussions = await _context.Discussions
                .Include(d => d.Comments)
                .Include(d => d.ApplicationUser)
                .ToListAsync();

            if (discussions == null || discussions.Count == 0)
            {
                return View(new List<Discussion>());
            }

            return View(discussions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GetDiscussion(int id)
        {
            //chatgpt assisted 
            var discussion = _context.Discussions
                .Include(d => d.Comments!.Where(c => c != null)).ThenInclude(c => c.ApplicationUser)
                .Include(d => d.ApplicationUser)
                .FirstOrDefault(d => d.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            ViewBag.CurrentUserId = _userManager.GetUserId(User);

            return View(discussion);
        }

        public async Task<IActionResult> Profile(string id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            var discussions = await _context.Discussions
                .Include(d => d.Comments)
                .Include(d => d.ApplicationUser)
                .Where(d => d.ApplicationUserId == id)
                .ToListAsync();

            if (user == null)
            {
                return NotFound();
            }

            var profileViewModel = new ProfileViewModel
            {
                User = user,
                Discussions = discussions
            };

            return View(profileViewModel);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

