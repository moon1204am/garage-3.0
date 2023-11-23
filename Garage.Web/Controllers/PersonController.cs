using Garage.Data.Data;
using Garage.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Garage.Web.Models.ViewModels;

namespace Garage.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly Garage2_0Context _context;
        const int PageSize = 3;

        public PersonController(Garage2_0Context context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index(int page = 0)
        {

            var selection = await _context.Person.Select(v => new PersonOverViewViewModel

            {
                PersonId = v.PersonId,
                FirstName = v.FirstName,
                LastName = v.LastName,
                SSN = v.SSN,
                NumberOfParkedVehicles = _context.Vehicle.Where(p => p.PersonId == v.PersonId).Count(),
            }).OrderByDescending(p => p.FirstName.Substring(0, 2))
             .ToListAsync();

            selection.Reverse();
            var count = selection.Count;

            var index = new PersonIndexViewModel
            {
                Members = selection.Skip(page * PageSize).Take(PageSize).ToList()
            };

            ViewBag.MaxPage = count / PageSize - (count % PageSize == 0 ? 1 : 0);
            ViewBag.Page = page;
            return View(index);
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

            var personDetailsViewModel = new PersonDetailsViewModel
            {
                SSN = person.SSN,
                FirstName = person.FirstName,
                LastName = person.LastName,
                PersonId = person.PersonId,
                Vehicles = await _context.Vehicle.Where(p => p.PersonId == person.PersonId).Include(v => v.VehicleType).ToListAsync()
            };

            return View(personDetailsViewModel);
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
        public async Task<IActionResult> Create(PersonViewModel personViewModel)
        {
            if (_context.Person.Any(p => p.SSN == personViewModel.SSN))
            {
                ModelState.AddModelError(nameof(personViewModel.SSN),
                                         "The number is already in used.");
            }

            //if (!IsValidDate(personViewModel.SSN))
            //{
            //    ModelState.AddModelError(nameof(personViewModel.SSN),
            //                            "Please enter a valid SSN");

            //}

            if (Under18Check(personViewModel.SSN))
            {
                ModelState.AddModelError(nameof(personViewModel.SSN),
                                         "You must be over 18 years old.");
            }

            if (personViewModel.FirstName == personViewModel.LastName)
            {
                ModelState.AddModelError(nameof(personViewModel.FirstName),
                                         "First name can be the same as last name.");
            }

            if (ModelState.IsValid)
            {
                var person = new Person
                {
                    SSN = personViewModel.SSN,
                    FirstName = personViewModel.FirstName,
                    LastName = personViewModel.LastName
                };
                _context.Add(person);
                await _context.SaveChangesAsync();
                TempData["OkFeedbackMsg"] = $"{person.FirstName} {person.LastName} has successfully registered as member.";
                return RedirectToAction(nameof(Index));
            }

            return View(personViewModel);
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

            if (person.FirstName.ToUpper() == person.LastName.ToUpper())
            {
                ModelState.AddModelError(nameof(person.FirstName),
                                         "First name can not be the same as last name.");
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

        private bool Under18Check(string ssn)
        {
            var birthday = $"{ssn.Substring(4, 2)}/{ssn.Substring(6, 2)}/{ssn.Substring(0, 4)}";
            var today = DateTime.Today.Date;
            var years = (today - DateTime.Parse(birthday)).Days / 365;

            if (years > 18) return false;
            return true;
        }

        private bool IsValidDate(string ssn)
        {
            DateTime birthday = DateTime.Parse(ssn.Substring(0,8));
            int year = birthday.Year;
            int month = birthday.Month;
            int day = birthday.Day;

            if (month >= 1 && month <= 12 && day >= 1 && day <= DateTime.DaysInMonth(year, month))
            {
                return true;
            }
            return false;
        }
        private bool PersonExists(int id)
        {
            return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Filter(PersonIndexViewModel personIndexViewModel, int page = 0)
        {

            var query = string.IsNullOrWhiteSpace(personIndexViewModel.LastName) ?
                                               _context.Person :
                                               _context.Person.Where(p => p.LastName.StartsWith(personIndexViewModel.LastName));

            var tempData = await query.Select(v => new PersonOverViewViewModel
            {
                PersonId = v.PersonId,
                FirstName = v.FirstName,
                LastName = v.LastName,
                SSN = v.SSN,
                NumberOfParkedVehicles = _context.Vehicle.Where(p => p.PersonId == v.PersonId).Count(),
            }).OrderByDescending(p => p.FirstName.Substring(0, 2))
             .ToListAsync();

            tempData.Reverse();
            var count = tempData.Count;

            var querySelect = new PersonIndexViewModel
            {
                Members = tempData.Skip(page * PageSize).Take(PageSize).ToList()
            };

            ViewBag.MaxPage = count / PageSize - (count % PageSize == 0 ? 1 : 0);
            ViewBag.Page = page;
            return View(nameof(Index), querySelect);
        }
    }
}
