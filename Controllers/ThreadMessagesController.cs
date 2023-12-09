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

namespace Forum.Controllers
{
    public class ThreadMessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThreadMessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ThreadMessages
        public async Task<IActionResult> Index()
        {
            return View(await _context.ThreadMessage.ToListAsync());
        }

        // GET: ThreadMessages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threadMessage = await _context.ThreadMessage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (threadMessage == null)
            {
                return NotFound();
            }

            return View(threadMessage);
        }

        // GET: ThreadMessages/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ThreadMessages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Content,UserId,ForumThreadId,Timestamp")] ThreadMessage threadMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(threadMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(threadMessage);
        }

        // GET: ThreadMessages/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threadMessage = await _context.ThreadMessage.FindAsync(id);
            if (threadMessage == null)
            {
                return NotFound();
            }
            return View(threadMessage);
        }

        // POST: ThreadMessages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,UserId,ForumThreadId,Timestamp")] ThreadMessage threadMessage)
        {
            if (id != threadMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(threadMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreadMessageExists(threadMessage.Id))
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
            return View(threadMessage);
        }

        // GET: ThreadMessages/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var threadMessage = await _context.ThreadMessage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (threadMessage == null)
            {
                return NotFound();
            }

            return View(threadMessage);
        }

        // POST: ThreadMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var threadMessage = await _context.ThreadMessage.FindAsync(id);
            if (threadMessage != null)
            {
                _context.ThreadMessage.Remove(threadMessage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThreadMessageExists(int id)
        {
            return _context.ThreadMessage.Any(e => e.Id == id);
        }
    }
}
