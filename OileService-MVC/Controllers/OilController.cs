using Microsoft.AspNetCore.Mvc;
using OilChangeApp.Data;

namespace OilChangeApp.Controllers
{
    public class OilController : Controller
    {
        private readonly OilChangeDbContext _context;

        public OilController(OilChangeDbContext context)
        {
            _context = context;
        }

        // GET: Oil
        public IActionResult Index()
        {
            var oils = _context.Oils.ToList();
            return View(oils);
        }

        // GET: Oil/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oil = _context.Oils.FirstOrDefault(o => o.Id == id);
            if (oil == null)
            {
                return NotFound();
            }
            return View(oil);
        }

        // GET: Oil/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Oil/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Description")] Oil oil)
        {
            if (ModelState.IsValid)
            {
                _context.Oils.Add(oil);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(oil);
        }

        // GET: Oil/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oil = _context.Oils.Find(id);
            if (oil == null)
            {
                return NotFound();
            }
            return View(oil);
        }

        // POST: Oil/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description")] Oil oil)
        {
            if (id != oil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(oil);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(oil);
        }

        // GET: Oil/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oil = _context.Oils.FirstOrDefault(o => o.Id == id);
            if (oil == null)
            {
                return NotFound();
            }
            return View(oil);
        }

        // POST: Oil/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var oil = _context.Oils.Find(id);
            _context.Oils.Remove(oil);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}