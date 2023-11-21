using Garage.Data.Data;
using Garage.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Garage2._0.Controllers
{
    public class PersonController : Controller
    {
        private readonly Garage2_0Context _context;

        public PersonController(Garage2_0Context context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
              return _context.Person != null ? 
                          View(await _context.Person.ToListAsync()) :
                          Problem("Entity set 'Garage2_0Context.Person'  is null.");
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,SSN,FirstName,LastName")] Person person)
        {
            //if (SSNExistedValidation(person.SSN))
            //{
            //    ModelState.AddModelError("SSN", "SSN is already existed.");
            //    return View();
            //}
            
            if (ModelState.IsValid && SSNExistedValidation(person.SSN) && CheckName(person.FirstName, person.LastName))
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                TempData["OkFeedbackMsg"] = $"{person.FirstName} {person.LastName} has successfully registered as member.";
                return RedirectToAction(nameof(Index));
            }
            
            return View(person);
        }

        private bool SSNExistedValidation(string sSN)
        {
            var person = _context.Person.FirstOrDefaultAsync(p => p.SSN == sSN);
            if (person == null) { return true; }
            return false;
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,SSN,FirstName,LastName")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                    TempData["OkFeedbackMsg"] = $"{person.FirstName} {person.LastName} has updated.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
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
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'Garage2_0Context.Person'  is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
                await _context.SaveChangesAsync();
                TempData["OkFeedbackMsg"] = $"{person.FirstName} {person.LastName} has closed the membership.";
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool CheckName(string firstName, string lastName)
        {
            return (firstName != lastName);
        }

        //private bool CheckAge(string ssn)
        //{
            
        //}
        
        private bool PersonExists(int id)
        {
          return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
    }
}
