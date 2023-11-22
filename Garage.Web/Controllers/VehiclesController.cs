using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage.Domain.Entities;
using Garage.Data.Data;
using AutoMapper;
using Garage.Web.Models.ViewModels;
using Garage.Web.Services;

namespace Garage.Web.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage2_0Context _context;
        private readonly IMapper mapper;
        private readonly IValidationService validation;

        public VehiclesController(Garage2_0Context context, IMapper mapper, IValidationService validation)
        {
            _context = context;
            this.mapper = mapper;
            this.validation = validation;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var model = new VehiclesOverviewViewModel
            {
                Vehicles = await GetAllVehicles()
            };
            return View(model);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Person)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVehicleViewModel createVehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                var vehicle = mapper.Map<Vehicle>(createVehicleViewModel);
                var owner = _context.Person.FirstOrDefault(p => p.SSN == createVehicleViewModel.PersonSSN);
                vehicle.Person = owner;
                _context.Add(vehicle);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createVehicleViewModel);
        }

        //Get
        public async Task<ActionResult> Park(VehiclesOverviewViewModel vehiclesOverviewView)
        {
            if (ModelState.IsValid)
            {
                var person = _context.Person.Include(p => p.Vehicles).FirstOrDefault(p => p.SSN == vehiclesOverviewView.SSN);
                if (person != null)
                {
                    var parkIndexViewModel = new ParkVehicleViewModel
                    {
                        Vehicles = GetPersonVehicles(person.Vehicles)
                    };

                    return View(parkIndexViewModel);
                }
            }
            vehiclesOverviewView.Vehicles = await GetAllVehicles();
            return View(nameof(Index), vehiclesOverviewView);
        }

        private async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            return await _context.Vehicle.Include(v => v.Person).Include(v => v.VehicleType).ToListAsync();
        }

        private IEnumerable<SelectListItem> GetPersonVehicles(IEnumerable<Vehicle> personVehicles)
        {

            return personVehicles.Where(v => v.IsParked == false).Select(v => new SelectListItem
            {
                Text = v.LicenseNr,
                Value = v.VehicleId.ToString()
            }).ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Park(ParkVehicleViewModel parkIndexViewModel)
        {

            if (ModelState.IsValid)
            {
                //get chosen vehicle to park
                var vehicleToPark = await _context.Vehicle.Include(v => v.Person).FirstOrDefaultAsync(v => v.VehicleId == parkIndexViewModel.VehicleId);
                // get free spots
                var freeParkingSpot = validation.FoundParkingSpot;
                // add spots to persons parkingspots list
                foreach (var spot in freeParkingSpot)
                {
                    spot.Arrival = DateTime.Now;
                    vehicleToPark.ParkingSpots.Add(spot);
                }
                vehicleToPark.IsParked = true;
                _context.Update(vehicleToPark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // FIX!!!!!!!!!!
            //parkIndexViewModel.Vehicles = GetPersonVehicles();
            return View(parkIndexViewModel);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "PersonId", "FirstName", vehicle.PersonId);
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "VehicleTypeId", "Size", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,LicenseNr,Color,Brand,Model,Wheels,Arrival,ParkingIndex,VehicleTypeId,PersonId")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
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
            ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "PersonId", "FirstName", vehicle.PersonId);
            ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "VehicleTypeId", "Size", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.Person)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'Garage2_0Context.Vehicle'  is null.");
            }
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return (_context.Vehicle?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }
    }
}
