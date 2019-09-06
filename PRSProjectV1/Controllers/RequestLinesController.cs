using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRSProjectV1.Models;

namespace PRSProjectV1.Controllers
{
    public class RequestLinesController : Controller
    {
        private readonly SqlPRSContext _context;

        public RequestLinesController(SqlPRSContext context)
        {
            _context = context;
        }

        // GET: RequestLines
        public async Task<IActionResult> Index()
        {
            var sqlPRSContext = _context.RequestLine.Include(r => r.Product).Include(r => r.Request);
            return View(await sqlPRSContext.ToListAsync());
        }

        // GET: RequestLines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestLine = await _context.RequestLine
                .Include(r => r.Product)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestLine == null)
            {
                return NotFound();
            }

            return View(requestLine);
        }

        // GET: RequestLines/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            ViewData["RequestId"] = new SelectList(_context.Request, "Id", "DeliveryMode");
            return View();
        }

        // POST: RequestLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestId,ProductId,Quantity")] RequestLine requestLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", requestLine.ProductId);
            ViewData["RequestId"] = new SelectList(_context.Request, "Id", "DeliveryMode", requestLine.RequestId);
            return View(requestLine);
        }

        // GET: RequestLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestLine = await _context.RequestLine.FindAsync(id);
            if (requestLine == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", requestLine.ProductId);
            ViewData["RequestId"] = new SelectList(_context.Request, "Id", "DeliveryMode", requestLine.RequestId);
            return View(requestLine);
        }

        // POST: RequestLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestId,ProductId,Quantity")] RequestLine requestLine)
        {
            if (id != requestLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestLineExists(requestLine.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", requestLine.ProductId);
            ViewData["RequestId"] = new SelectList(_context.Request, "Id", "DeliveryMode", requestLine.RequestId);
            return View(requestLine);
        }

        // GET: RequestLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var requestLine = await _context.RequestLine
                .Include(r => r.Product)
                .Include(r => r.Request)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (requestLine == null)
            {
                return NotFound();
            }

            return View(requestLine);
        }

        // POST: RequestLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var requestLine = await _context.RequestLine.FindAsync(id);
            _context.RequestLine.Remove(requestLine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestLineExists(int id)
        {
            return _context.RequestLine.Any(e => e.Id == id);
        }
    }
}
