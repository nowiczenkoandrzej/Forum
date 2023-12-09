using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum.Data;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Forum.Controllers
{
    public class ForumThreadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForumThreadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ForumThreads
        public async Task<IActionResult> Index()
        {
            return View(await _context.ForumThread.ToListAsync());
        }

        // GET: ForumThreads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumThread = await _context.ForumThread
                .Include(t => t.Messages )
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumThread == null)
            {
                return NotFound();
            }

            return View(forumThread);
        }

        // GET: ForumThreads/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ForumThreads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,userId")] ForumThread forumThread)
        {
            if (ModelState.IsValid)
            {
                forumThread.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(forumThread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forumThread);
        }

        // GET: ForumThreads/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumThread = await _context.ForumThread.FindAsync(id);
            if (forumThread == null)
            {
                return NotFound();
            }
            return View(forumThread);
        }

        // POST: ForumThreads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] ForumThread forumThread)
        {
            if (id != forumThread.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumThread);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumThreadExists(forumThread.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(forumThread);
        }

        // GET: ForumThreads/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var forumThread = await _context.ForumThread
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumThread == null)
            {
                return NotFound();
            }

            return View(forumThread);
        }

        // POST: ForumThreads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var forumThread = await _context.ForumThread.FindAsync(id);
            if (forumThread != null)
            {
                _context.ForumThread.Remove(forumThread);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumThreadExists(int id)
        {
            return _context.ForumThread.Any(e => e.Id == id);
        }
    }
}
