using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GalaxyForum.Data;
using GalaxyForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GalaxyForum.Controllers
{
    [Authorize]
    public class DiscussionController : Controller
    {
        private readonly GalaxyForumContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DiscussionController(GalaxyForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Discussion
        //public async Task<IActionResult> Index()
        //{
        //    return View( await _context.Discussions
        //        .Include(d => d.Comments)
        //        .Where(d => d.ApplicationUserId == _userManager.GetUserId(User))
        //        .ToListAsync());
        //}

        // GET: Discussion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussions
                .Include(d => d.Comments)
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // GET: Discussion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Discussion/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title, Content")] Discussion discussion, IFormFile? ImageFile = null)
        {
            discussion.ApplicationUserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);

                    string filePath = Path.Combine("wwwroot", "images", newFileName);
                    string relativePath = Path.Combine("images", newFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    discussion.ImageFilename = relativePath;
                }

                discussion.CreateDate = DateTime.Now;

                _context.Add(discussion);
                await _context.SaveChangesAsync();

                return RedirectToAction("GetDiscussion", "Home", new { id = discussion.DiscussionId });
            }
            return View(discussion);
        }



        // GET: Discussion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussions.FindAsync(id);
            if (discussion == null)
            {
                return NotFound();
            }

            if (discussion.ApplicationUserId != _userManager.GetUserId(User))
            {
                Console.WriteLine("current user    = " + _userManager.GetUserId(User));
                Console.WriteLine("discussion user = " + discussion.ApplicationUserId);
                return Forbid();
            }

            return View(discussion);
        }

        // POST: Discussion/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("DiscussionId,Title,Content,ImageFilename,ApplicationUserId")] Discussion discussion, IFormFile? ImageFile = null)
        {
            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }

            if (discussion.ApplicationUserId != _userManager.GetUserId(User))
            {
                Console.WriteLine("current user    = " + _userManager.GetUserId(User));
                Console.WriteLine("discussion user = " + discussion.ApplicationUserId);
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);

                        string filePath = Path.Combine("wwwroot", "images", newFileName);
                        string relativePath = Path.Combine("images", newFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        discussion.ImageFilename = relativePath;
                    }

                    _context.Update(discussion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionExists(discussion.DiscussionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("GetDiscussion", "Home", new { id = discussion.DiscussionId });
            }
            return View(discussion);
        }


        // GET: Discussion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = await _context.Discussions
                .FirstOrDefaultAsync(m => m.DiscussionId == id);
            if (discussion == null)
            {
                return NotFound();
            }

            if (discussion.ApplicationUserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            return View(discussion);
        }

        // POST: Discussion/Delete/5

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discussion = await _context.Discussions.FindAsync(id);
            if (discussion != null)
            {
                if (discussion.ApplicationUserId != _userManager.GetUserId(User))
                {
                    return Forbid();
                }

                _context.Discussions.Remove(discussion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionExists(int id)
        {
            return _context.Discussions.Any(e => e.DiscussionId == id);
        }

        // GET: Discussion/AddComment/5
        public IActionResult AddComment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussion = _context.Discussions.Find(id);
            if (discussion == null)
            {
                return NotFound();
            }

            var comment = new Comment
            {
                DiscussionId = id.Value
            };

            return View(comment);
        }

        // POST: Discussion/AddComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreateDate = DateTime.Now;
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = comment.DiscussionId });
            }

            return View(comment);
        }


    }
}
