using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Garage.Domain.Entities;
using Garage.Data.Data;
using Garage.Web.Models.ViewModels;
using Garage.Data;

namespace Garage.Web.Controllers
{
    public class GarageController : Controller
    {
        private readonly Garage2_0Context _context;
        
        public GarageController (Garage2_0Context context)
        {
            _context = context;
           
           
        }
        public async Task<IActionResult> Statistics()
        {
            var fordon = await _context.Vehicle.Include(v => v.VehicleType).ToListAsync();
            var parkeradeFordon = fordon.Where(p => p.IsParked);
            var occupiedParkingSpots = await _context.ParkingSpot.Where(s => s.VehicleId != null).ToListAsync();
            var availableParkingSpots = await _context.ParkingSpot.Where(s => s.VehicleId == null).ToListAsync();
            var statistikModell = new StatisticsViewModel();
            double totalaAntaletMinuter = 0;
            int antalParkeradeFordon = parkeradeFordon.Count();
            var summaHjul = parkeradeFordon.Sum(v => v.Wheels);
            string spots = null;
            


                    foreach (var item in occupiedParkingSpots)      
                    {
                        totalaAntaletMinuter += RaknaUtTid(item.Arrival, DateTime.Now).TotalMinutes;                       
                    }
                     foreach (var item in availableParkingSpots)
                     {
                         spots += " " + item.ParkingSpotId;
                     }


            AntalFordonPerSort(statistikModell, parkeradeFordon);
            statistikModell.AntalHjulIGaraget = summaHjul;
            statistikModell.Intakter = totalaAntaletMinuter * GarageSettings.pricePerMinute;
            statistikModell.GenomsnittligParkeradTid = (int)(totalaAntaletMinuter / antalParkeradeFordon);
            return View(statistikModell);
        }

        private static StatisticsViewModel AntalFordonPerSort(StatisticsViewModel statistikModell, IEnumerable<Vehicle> parkeradeFordon)
        {

            statistikModell.AntalBatar = parkeradeFordon.Where(p => p.VehicleType.VehicleTypeId == 2).Count();
            statistikModell.AntalBilar = parkeradeFordon.Where(p => p.VehicleType.VehicleTypeId == 4).Count();
            statistikModell.AntalBussar = parkeradeFordon.Where(p => p.VehicleType.VehicleTypeId == 3).Count();
            statistikModell.AntalFlygplan = parkeradeFordon.Where(p => p.VehicleType.VehicleTypeId == 1).Count();
            statistikModell.AntalMotorcyklar = parkeradeFordon.Where(p => p.VehicleType.VehicleTypeId == 5).Count();
            return (statistikModell);
        }

        private TimeSpan RaknaUtTid(DateTime ankomst, DateTime utckeck)
        {
            return utckeck.Subtract(ankomst);
        }

        public async Task<IActionResult> AvailableParkingSpots()
        {
            var availableParkingSpots = await _context.ParkingSpot.Where(s => s.VehicleId == null).ToListAsync();
            
            var availableParkingSpotsViewModel = new AvailableSpotsViewModel();
           
            foreach (var item in availableParkingSpots)
            {
                availableParkingSpotsViewModel.AvailableParkingSpots += " " + item.ParkingSpotId;
            }

            return View(availableParkingSpotsViewModel);

        }


        }
}
