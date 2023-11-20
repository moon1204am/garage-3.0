using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage.Domain.Entities;
using Garage.Data.Data;
using Garage3._0.Models.ViewModels;
using AutoMapper;
using Garage.Data;

namespace Garage3._0.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage2_0Context _context;
        private readonly IMapper mapper;

        public VehiclesController(Garage2_0Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var garage2_0Context = _context.Vehicle.Include(v => v.Person).Include(v => v.VehicleType);
            return View(await garage2_0Context.ToListAsync());
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
        public async Task<ActionResult> Create()
        {
            var vehicles = await _context.VehicleType.ToListAsync();

            var createIndexViewModel = new CreateVehicleViewModel
            {
                VehicleTypes = GetVehicles(vehicles)
            };

            //ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "PersonId", "FirstName");
            //ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "VehicleTypeId", "Size");
            return View(createIndexViewModel);
        }

        private static List<SelectListItem> GetVehicles(List<VehicleType> vehicleTypes)
        {
            return vehicleTypes.Select(t => new SelectListItem
                                           {
                                               Text = t.Type.ToString(),
                                               Value = t.VehicleTypeId.ToString()
                                           })
                                           .ToList();
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

                var parkingSpot = _context.ParkingSpot.Where(p => p.ParkingSpotId != null).FirstOrDefault();
                if(parkingSpot != null)
                {
                    parkingSpot.Arrival = DateTime.Now;
                    vehicle.ParkingSpots.Add(parkingSpot);

                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["PersonId"] = new SelectList(_context.Set<Person>(), "PersonId", "FirstName", vehicle.PersonId);
            //ViewData["VehicleTypeId"] = new SelectList(_context.Set<VehicleType>(), "VehicleTypeId", "Size", vehicle.VehicleTypeId);
            return View(createVehicleViewModel);
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
