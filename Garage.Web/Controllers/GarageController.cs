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
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        public async Task<IActionResult> Statistik()
        {
            var fordon = await _context.Vehicle.Include(v => v.VehicleType).ToListAsync();
            var parkeradeFordon = fordon.Where(p => p.IsParked);
            var parkingSpots = await _context.ParkingSpot.ToListAsync();
            var statistikModell = new StatistikViewModel();
            double totalaAntaletMinuter = 0;
            int antalParkeradeFordon = parkeradeFordon.Count();
            var summaHjul = parkeradeFordon.Sum(v => v.Wheels);


            foreach (var item in parkingSpots)
                if (item.VehicleId != null)
                {

                    {
                        totalaAntaletMinuter += RaknaUtTid(item.Arrival, DateTime.Now).TotalMinutes;
                    }
                }

            AntalFordonPerSort(statistikModell, parkeradeFordon);
            statistikModell.AntalHjulIGaraget = summaHjul;
            statistikModell.Intakter = totalaAntaletMinuter * GarageSettings.pricePerMinute;
            statistikModell.GenomsnittligParkeradTid = (int)(totalaAntaletMinuter / antalParkeradeFordon);
            return View(statistikModell);
        }

        private static StatistikViewModel AntalFordonPerSort(StatistikViewModel statistikModell, IEnumerable<Vehicle> parkeradeFordon)
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
    }
}
