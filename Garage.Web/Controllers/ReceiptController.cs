using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage.Data.Data;
using Garage.Domain.Entities;
using Garage.Web.Models.ViewModels;

namespace Garage.Web.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly Garage2_0Context _context;
        private const int price = 120;

        public ReceiptController(Garage2_0Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null) { return NotFound(); }
            var parkingVehicle = await _context.Vehicle.Include(v => v.ParkingSpots)
                                .FirstOrDefaultAsync(v => v.VehicleId == id);
            var parkingSpot = parkingVehicle.ParkingSpots.FirstOrDefault(v => v.VehicleId == id);
            
            //_context.Vehicle.Remove(parkingVehicle);
            //await _context.SaveChangesAsync();
            //TempData["OkFeedbackMsg"] = $"{parkingVehicle.LicenseNr} has checked out.";

            DateTime checkOut = DateTime.Now;
            TimeSpan time = GetTime(parkingSpot.Arrival, checkOut);
            string parkingTime = $"{time.Hours} hours {time.Minutes} minutes";
            int totalCost = GetTotalCost(price, time);

            var model = new ReceiptViewModel
            {
                ParkingSpotID = parkingSpot.ParkingSpotId,
                LicenseNr = parkingVehicle.LicenseNr,
                Name = $"{parkingVehicle.Person.FirstName} {parkingVehicle.Person.LastName}",
                Arrival = parkingSpot.Arrival,
                CheckOut = checkOut,
                ParkingTime = parkingTime,
                Price = price,
                TotalCost = totalCost
            };
            return View(model);
        }

        private TimeSpan GetTime(DateTime arrival, DateTime checkOut)
        {
            return checkOut.Subtract(arrival);
        }

        private int GetTotalCost(int price, TimeSpan parkingTime)
        {
            return (int)parkingTime.TotalMinutes * (price / 60);
        }


        // GET: Receipt
        public async Task<IActionResult> Index()
        {
            var garage2_0Context = _context.ParkingSpot.Include(p => p.Vehicle);
            return View(await garage2_0Context.ToListAsync());
        }

        // GET: Receipt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParkingSpot == null)
            {
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpot
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.ParkingSpotId == id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            return View(parkingSpot);
        }

        // GET: Receipt/Create
        public IActionResult Create()
        {
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Brand");
            return View();
        }

        // POST: Receipt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParkingSpotId,Arrival,VehicleId")] ParkingSpot parkingSpot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingSpot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Brand", parkingSpot.VehicleId);
            return View(parkingSpot);
        }

        // GET: Receipt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParkingSpot == null)
            {
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpot.FindAsync(id);
            if (parkingSpot == null)
            {
                return NotFound();
            }
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Brand", parkingSpot.VehicleId);
            return View(parkingSpot);
        }

        // POST: Receipt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParkingSpotId,Arrival,VehicleId")] ParkingSpot parkingSpot)
        {
            if (id != parkingSpot.ParkingSpotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingSpot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingSpotExists(parkingSpot.ParkingSpotId))
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
            ViewData["VehicleId"] = new SelectList(_context.Vehicle, "VehicleId", "Brand", parkingSpot.VehicleId);
            return View(parkingSpot);
        }

        // GET: Receipt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ParkingSpot == null)
            {
                return NotFound();
            }

            var parkingSpot = await _context.ParkingSpot
                .Include(p => p.Vehicle)
                .FirstOrDefaultAsync(m => m.ParkingSpotId == id);
            if (parkingSpot == null)
            {
                return NotFound();
            }

            return View(parkingSpot);
        }

        // POST: Receipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ParkingSpot == null)
            {
                return Problem("Entity set 'Garage2_0Context.ParkingSpot'  is null.");
            }
            var parkingSpot = await _context.ParkingSpot.FindAsync(id);
            if (parkingSpot != null)
            {
                _context.ParkingSpot.Remove(parkingSpot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

       

        private bool ParkingSpotExists(int id)
        {
          return (_context.ParkingSpot?.Any(e => e.ParkingSpotId == id)).GetValueOrDefault();
        }
    }
}
