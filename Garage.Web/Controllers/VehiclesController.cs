using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage.Domain.Entities;
using Garage.Data.Data;
using AutoMapper;
using Garage.Web.Models.ViewModels;
using Garage.Web.Services;
using Garage.Data;

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
            var vehicles = await GetAllParkedVehicles();
            var model = new VehiclesOverviewViewModel
            {
                ParkedVehiclesViewModel = vehicles,
                FreeSpots = GarageSettings.capacity - vehicles.Count()
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
            vehiclesOverviewView.ParkedVehiclesViewModel = await GetAllParkedVehicles();
            return View(nameof(Index), vehiclesOverviewView);
        }

        private async Task<IEnumerable<ParkedVehiclesViewModel>> GetAllParkedVehicles()
        {
            var parkedVehicles = _context.Vehicle.Include(v => v.Person).Include(v => v.VehicleType).Include(v => v.ParkingSpots).Where(v => v.IsParked == true);

            return await parkedVehicles.Select(p => new ParkedVehiclesViewModel
                                            {
                                                VehicleId = p.VehicleId,
                                                FirstName = p.Person.FirstName,
                                                LastName =p.Person.LastName,
                                                VehicleType = p.VehicleType.Type,
                                                LicenseNr = p.LicenseNr,
                                                ParkingTime = DateTime.Now - parkedVehicles.FirstOrDefault(v => v.VehicleId == p.VehicleId).ParkingSpots.Select(ps => ps.Arrival).FirstOrDefault()

                                            }).ToListAsync();
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

            var person = _context.Person.Include(p => p.Vehicles).FirstOrDefault(p => p.SSN == parkIndexViewModel.SSN);
            parkIndexViewModel.Vehicles = GetPersonVehicles(person.Vehicles);
            return View(parkIndexViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ParkMember(int id)
        {
            var size = validation.GetSizeFromId(id);
            var freeSpotsExists = validation.ParkingSpaceExists(size);

            if(!freeSpotsExists)
            {
                ModelState.AddModelError("No free spots exists.", freeSpotsExists.ToString());
                return View();
            }
            if (ModelState.IsValid)
            {
                var vehicleToPark = await _context.Vehicle.Include(v => v.Person).FirstOrDefaultAsync(v => v.VehicleId == id);
                var freeParkingSpot = validation.FoundParkingSpot;

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
            return View(nameof(Details), nameof(Person));
        }

        public async Task<IActionResult> Checkout(int? id) 
        { 
            if(id == null)
            {
                return NotFound();
            }
            var vehicleToCheckout = await _context.Vehicle
                .FirstOrDefaultAsync(v => v.VehicleId == id);

            if (vehicleToCheckout == null)
            {
                return NotFound();
            }
            return View(vehicleToCheckout); 
        }

        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(int id)
        {
            var vehicleToCheckout = _context.Vehicle.Include(v => v.ParkingSpots).FirstOrDefault(v => v.VehicleId == id);

            ReceiptViewModel receipt = null;
            if (vehicleToCheckout != null && vehicleToCheckout.ParkingSpots != null)
            {
                foreach(var spot in vehicleToCheckout.ParkingSpots.ToList())
                {
                    vehicleToCheckout.ParkingSpots.Remove(spot);
                }
                vehicleToCheckout.IsParked = false;
                
                await _context.SaveChangesAsync();

                receipt = Receipt(vehicleToCheckout);
            }

            return View(nameof(Receipt), receipt);
        }

        public IActionResult ParkExisting()
        {
            return View();
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

        public async Task<IActionResult> Filter(VehiclesOverviewViewModel vehiclesOverviewViewModel)
        {
            //var vehiclesParked = _context.Vehicle.Include(v => v.VehicleType).Include(v => v.Person).Include(p => p.ParkingSpots).Where(v => v.IsParked == true);

            var vehicles = string.IsNullOrWhiteSpace(vehiclesOverviewViewModel.LicenseNr) ?
                                               _context.Vehicle.Include(v => v.VehicleType).Include(v => v.Person).Include(p => p.ParkingSpots).Where(v => v.IsParked == true) :
                                               _context.Vehicle.Include(v => v.VehicleType).Include(v => v.Person).Include(p => p.ParkingSpots).Where(v => v.IsParked == true).Where(v => v.LicenseNr.StartsWith(vehiclesOverviewViewModel.LicenseNr));

            vehicles = vehiclesOverviewViewModel.VehicleTypeId is null ? vehicles : vehicles.Where(v => v.VehicleType.VehicleTypeId == vehiclesOverviewViewModel.VehicleTypeId);

            var result = await vehicles.Select(p => new ParkedVehiclesViewModel
            {
                FirstName = p.Person.FirstName,
                LastName = p.Person.LastName,
                VehicleId = p.VehicleId,
                VehicleType = p.VehicleType.Type,
                LicenseNr = p.LicenseNr,
                ParkingTime = DateTime.Now - vehicles.FirstOrDefault(v => v.VehicleId == p.VehicleId).ParkingSpots.Select(ps => ps.Arrival).FirstOrDefault()
            }).ToListAsync();

            var vehicleModel = new VehiclesOverviewViewModel
            {
                ParkedVehiclesViewModel = result,
                FreeSpots = GarageSettings.capacity - vehicles.Count(),
            };
            return View(nameof(Index), vehicleModel);
        }

        public IActionResult CreateVehicleType()
        {
            return View();
        }

        // POST: VehicleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVehicleType([Bind("VehicleTypeId,Type,Size")] VehicleType vehicleType)
        {
            
            if (_context.VehicleType.Any(p => p.Type == vehicleType.Type))
            {
                ModelState.AddModelError(nameof(vehicleType.Type),
                                         "The Type already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(vehicleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vehicleType);
        }

        public ReceiptViewModel Receipt(Vehicle vehicle)
        {
            //var parkingVehicle = await _context.Vehicle.Include(v => v.ParkingSpots)
            //                    .FirstOrDefaultAsync(v => v.VehicleId == id);
            var parkingSpot = vehicle.ParkingSpots.FirstOrDefault(v => v.VehicleId == vehicle.VehicleId);

            //_context.Vehicle.Remove(parkingVehicle);
            //await _context.SaveChangesAsync();
            //TempData["OkFeedbackMsg"] = $"{parkingVehicle.LicenseNr} has checked out.";

            DateTime checkOut = DateTime.Now;
            TimeSpan time = GetTime(parkingSpot.Arrival, checkOut);
            string parkingTime = $"{time.Hours} hours {time.Minutes} minutes";
            int totalCost = GetTotalCost(GarageSettings.pricePerHour, time);

            var model = new ReceiptViewModel
            {
                ParkingSpotID = parkingSpot.ParkingSpotId,
                LicenseNr = vehicle.LicenseNr,
                Name = $"{vehicle.Person.FirstName} {vehicle.Person.LastName}",
                Arrival = parkingSpot.Arrival,
                CheckOut = checkOut,
                ParkingTime = parkingTime,
                Price = GarageSettings.pricePerHour,
                TotalCost = totalCost
            };
            return model;
        }

        private TimeSpan GetTime(DateTime arrival, DateTime checkOut)
        {
            return checkOut.Subtract(arrival);
        }

        private int GetTotalCost(int price, TimeSpan parkingTime)
        {
            return (int)parkingTime.TotalMinutes * (price / 60);
        }
    }
}
