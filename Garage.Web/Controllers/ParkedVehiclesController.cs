//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Garage2._0.Models.ViewModels;
//using System.Text;
//using Garage.Domain.Entities;

//namespace Garage2._0.Controllers
//{
//    public class ParkedVehiclesController : Controller
//    {
//        //private readonly Garage2_0Context _context;
//        private const int timPris = 120;
//        private const int minutPris = 2;
//        private const int capacity = 100;
//        private double[] garage = new double[capacity];
//        private double antal;
//        private const double enMcPlats = 1 / 3d;
//        private const double tvaMcPlats = 2 / 3d;
//        private const double enParkeringsPlats = 1d;
//        private const double ledigParkeringsPlats = 0d;

//        public ParkedVehiclesController(Garage2_0Context context)
//        {
//            _context = context;
//            //InitGarage();
//        }

//        public double AntalFordonIGaraget => antal;

//        // GET: ParkeratFordons
//        public async Task<IActionResult> Index()
//        {
//            var fordon = await _context.ParkeratFordon.Select(v => new FordonOversiktViewModel
//            {
//                Id = v.VehicleId,
//                FordonsTyp = v.VehicleType,
//                RegNr = v.LicenseNr,
//                AnkomstTid = v.Arrival

//            }).ToListAsync();

//            var index = new StartsidaViewModel
//            {
//                ParkeradeFordon = fordon,
//                AntalLedigaPlatser = capacity - antal
//            };

//            return View(index);
//        }

//        public IActionResult GaragePlatser()
//        {
//            StringBuilder sb = new StringBuilder();
//            int i = 1;
//            foreach (var item in garage)
//            {
//                if (item == ledigParkeringsPlats)
//                    sb.AppendLine($"Plats {i}");
//                else if (item == Math.Round(enMcPlats, 2))
//                {
//                    sb.AppendLine($"Plats {i}");
//                    sb.AppendLine($"Två lediga motorcykelplatser");
//                }
//                else if (item == Math.Round(tvaMcPlats, 2))
//                {
//                    sb.AppendLine($"Plats {i}");
//                    sb.AppendLine($"En ledig motorcykel plats");
//                }
//                i++;
//            }
//            var model = new GarageOversiktViewModel
//            {
//                LedigaPlatser = sb.ToString(),
//                AntalLedigaPlatser = capacity - antal
//            };
//            return View(model);
//        }

//        //private void InitGarage()
//        //{
//        //    var fordon = _context.ParkeratFordon.ToList();
//        //    int index = 0;
//        //    foreach (var f in fordon)
//        //    {
//        //        switch (fordon[index].FordonsTyp)
//        //        {
//        //            case VehicleType.Flygplan:
//        //            case VehicleType.Bat:
//        //                garage[f.ParkeringsIndex] = enParkeringsPlats;
//        //                garage[f.ParkeringsIndex + 1] = enParkeringsPlats;
//        //                garage[f.ParkeringsIndex + 2] = enParkeringsPlats;
//        //                antal += 3;
//        //                break;
//        //            case VehicleType.Bil:
//        //                garage[f.ParkeringsIndex] = enParkeringsPlats;
//        //                antal++;
//        //                break;
//        //            case VehicleType.Motorcykel:
//        //                garage[f.ParkeringsIndex] += Math.Round(enMcPlats, 2);
//        //                antal += Math.Round(enMcPlats, 2);
//        //                break;
//        //            case VehicleType.Buss:
//        //                garage[f.ParkeringsIndex] = enParkeringsPlats;
//        //                garage[f.ParkeringsIndex + 1] = enParkeringsPlats;
//        //                antal += 2;
//        //                break;
//        //        }
//        //        index++;
//        //    }
//        //}

//        // GET: ParkeratFordons/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null || _context.ParkeratFordon == null)
//            {
//                return NotFound();
//            }

//            var parkeratFordon = await _context.ParkeratFordon
//                .FirstOrDefaultAsync(m => m.VehicleId == id);
//            if (parkeratFordon == null)
//            {
//                return NotFound();
//            }

//            return View(parkeratFordon);
//        }

//        // GET: ParkeratFordons/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: ParkeratFordons/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create(FordonViewModel fordonViewModel)
//        {
//            if (RegNrExisterarValidering(fordonViewModel.RegNr))
//            {
//                ModelState.AddModelError("RegNr", "Registreringsnumret existerar redan.");
//                return View(fordonViewModel);
//            }

//            if (ModelState.IsValid)
//            {
//                var fordon = new Vehicle
//                {
//                    VehicleType = fordonViewModel.FordonsTyp,
//                    LicenseNr = fordonViewModel.RegNr,
//                    Color = fordonViewModel.Farg,
//                    Brand = fordonViewModel.Marke,
//                    Model = fordonViewModel.Modell,
//                    Wheels = fordonViewModel?.AntalHjul,
//                    Arrival = DateTime.Now
//                };
//                _context.Add(fordon);
//                await _context.SaveChangesAsync();
//                TempData["OkFeedbackMsg"] = $"Parkerat fordon med reg nr {fordonViewModel.RegNr}";
//                return RedirectToAction(nameof(Index));
//            }
//            return View(fordonViewModel);
//        }

//        [AcceptVerbs("GET", "POST")]
//        public IActionResult RegNrExisterar(string regNr, string tidigareRegNr)
//        {
//            var fordon = _context.ParkeratFordon.FirstOrDefault(v => v.LicenseNr == regNr);
//            if (fordon == null || regNr == tidigareRegNr)
//            {
//                return Json(true);
//            }
//            return Json($"Registreringsnumret existerar redan.");
//        }

//        // GET: ParkeratFordons/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null || _context.ParkeratFordon == null)
//            {
//                return NotFound();
//            }

//            var parkeratFordon = await _context.ParkeratFordon.FindAsync(id);

//            if (parkeratFordon == null)
//            {
//                return NotFound();
//            }

//            var fordonViewModel = new FordonViewModel
//            {
//                RegNr = parkeratFordon.LicenseNr,
//                AntalHjul = parkeratFordon.Wheels,
//                Modell = parkeratFordon.Model,
//                FordonsTyp = parkeratFordon.VehicleType,
//                Farg = parkeratFordon.Color,
//                Marke = parkeratFordon.Brand
//            };

//            return View(fordonViewModel);
//        }

//        // POST: ParkeratFordons/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, FordonViewModel parkeratFordonViewModel)
//        {
//            if (id != parkeratFordonViewModel.Id)
//            {
//                return NotFound();
//            }

//            if (RegNrExisterarValidering(parkeratFordonViewModel.RegNr))
//            {
//                ModelState.AddModelError("RegNr", "Registreringsnumret existerar redan.");
//                return View(parkeratFordonViewModel);
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    var parkeratFordon = await _context.ParkeratFordon.FindAsync(id);
//                    if (parkeratFordon != null)
//                    {
//                        parkeratFordon.VehicleType = parkeratFordonViewModel.FordonsTyp;
//                        parkeratFordon.LicenseNr = parkeratFordonViewModel.RegNr;
//                        parkeratFordon.Color = parkeratFordonViewModel.Farg;
//                        parkeratFordon.Brand = parkeratFordonViewModel.Marke;
//                        parkeratFordon.Model = parkeratFordonViewModel.Modell;
//                        parkeratFordon.Wheels = parkeratFordonViewModel.AntalHjul;
//                        _context.Update(parkeratFordon);


//                        TempData["OkFeedbackMsg"] = $"Uppdaterat fordon med reg nr {parkeratFordonViewModel.RegNr}";
//                    }
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!ParkeratFordonExists(parkeratFordonViewModel.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(parkeratFordonViewModel);
//        }

//        // GET: ParkeratFordons/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null || _context.ParkeratFordon == null)
//            {
//                return NotFound();
//            }

//            var parkeratFordon = await _context.ParkeratFordon
//                .FirstOrDefaultAsync(m => m.VehicleId == id);
//            if (parkeratFordon == null)
//            {
//                return NotFound();
//            }

//            return View(parkeratFordon);
//        }


//        // POST: ParkeratFordons/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var parkeratFordon = await _context.ParkeratFordon.FindAsync(id);
//            DateTime utcheckTid = DateTime.Now;
//            TimeSpan tid = RaknaUtTid(parkeratFordon.Arrival, utcheckTid);

//            int totalPris = RaknaUtPris(minutPris, tid);

//            if (parkeratFordon != null)
//            {
//                _context.ParkeratFordon.Remove(parkeratFordon);
//                await _context.SaveChangesAsync();
//                TempData["OkFeedbackMsg"] = $"Hämtar fordon med reg nr {parkeratFordon.LicenseNr} Kostnad {totalPris} kr";
//            }
//            return RedirectToAction(nameof(Index));
//        }

//        public async Task<IActionResult> Kvitto(int? id)
//        {
//            if (id == null) { return NotFound(); }
//            var parkeratFordon = await _context.ParkeratFordon
//               .FirstOrDefaultAsync(m => m.VehicleId == id);
//            _context.ParkeratFordon.Remove(parkeratFordon);
//            await _context.SaveChangesAsync();
//            TempData["OkFeedbackMsg"] = $"Hämtat fordon med reg nr {parkeratFordon.LicenseNr} samt kvitto.";

//            DateTime utcheckTid = DateTime.Now;
//            TimeSpan tid = RaknaUtTid(parkeratFordon.Arrival, utcheckTid);
//            string parkeringsTid = $"{tid.Hours} tim {tid.Minutes} min";
//            int totalPris = RaknaUtPris(minutPris, tid);

//            var model = new KvittoViewModel
//            {
//                RegNr = parkeratFordon.LicenseNr,
//                AnkomstTid = parkeratFordon.Arrival,
//                UtchecksTid = utcheckTid,
//                ParkeringsTid = parkeringsTid,
//                Pris = timPris,
//                TotalPris = totalPris
//            };
//            return View(model);
//        }

//        private TimeSpan RaknaUtTid(DateTime ankomst, DateTime utckeck)
//        {
//            return utckeck.Subtract(ankomst);
//        }

//        private int RaknaUtPris(int pris, TimeSpan parkeringstid)
//        {
//            return (int)parkeringstid.TotalMinutes * pris;
//        }

//        private bool ParkeratFordonExists(int id)
//        {
//            return (_context.ParkeratFordon?.Any(e => e.VehicleId == id)).GetValueOrDefault();
//        }

//        public async Task<IActionResult> Filter(StartsidaViewModel startsidaViewModel)
//        {
//            var utvalda = string.IsNullOrWhiteSpace(startsidaViewModel.RegNr) ?
//                                               _context.ParkeratFordon :
//                                               _context.ParkeratFordon.Where(p => p.LicenseNr.StartsWith(startsidaViewModel.RegNr));

//            utvalda = startsidaViewModel.FordonsTyp is null ? utvalda : utvalda.Where(v => v.VehicleType == startsidaViewModel.FordonsTyp);

//            var valdaFordon = await utvalda.Select(v => new FordonOversiktViewModel
//            {
//                Id = v.VehicleId,
//                FordonsTyp = v.VehicleType,
//                RegNr = v.LicenseNr,
//                AnkomstTid = v.Arrival
//            }).ToListAsync();

//            var fordon = new StartsidaViewModel
//            {
//                ParkeradeFordon = valdaFordon,
//                AntalLedigaPlatser = capacity - antal,
//            };
//            return View(nameof(Index), fordon);
//        }

//        private bool RegNrExisterarValidering(string regNr)
//        {
//            string tidigare = Request.Form["tidigareRegNr"];
//            var fordonReg = _context.ParkeratFordon.FirstOrDefault(v => v.LicenseNr == regNr);
//            if (fordonReg == null || regNr == tidigare)
//            {
//                return false;
//            }
//            return true;
//        }

//        public async Task<IActionResult> Statistik()
//        {
//            var parkeradeFordon = await _context.ParkeratFordon.ToListAsync();
//            var statistikModell = new StatistikViewModel();
//            double totalaAntaletMinuter = 0;
//            double antalParkeradeFordon = parkeradeFordon.Count;
//            var summaHjul = parkeradeFordon.Sum(v => v.Wheels);

//            foreach (var item in parkeradeFordon)
//            {
//                totalaAntaletMinuter += RaknaUtTid(item.Arrival, DateTime.Now).TotalMinutes;
//            }

//            AntalFordonPerSort(statistikModell, parkeradeFordon);
//            statistikModell.AntalHjulIGaraget = summaHjul;
//            statistikModell.Intakter = totalaAntaletMinuter * minutPris;
//            statistikModell.GenomsnittligParkeradTid = (int)(totalaAntaletMinuter / antalParkeradeFordon);
//            return View(statistikModell);
//        }

//        private static StatistikViewModel AntalFordonPerSort(StatistikViewModel statistikModell, IEnumerable<Vehicle> parkeradeFordon)
//        {

//            //statistikModell.AntalBatar = parkeradeFordon.Where(p => p.VehicleType.Equals(VehicleType.Bat)).Count();
//            //statistikModell.AntalBilar = parkeradeFordon.Where(p => p.VehicleType.Equals(VehicleType.Bil)).Count();
//            //statistikModell.AntalBussar = parkeradeFordon.Where(p => p.VehicleType.Equals(VehicleType.Buss)).Count();
//            //statistikModell.AntalFlygplan = parkeradeFordon.Where(p => p.VehicleType.Equals(VehicleType.Flygplan)).Count();
//            //statistikModell.AntalMotorcyklar = parkeradeFordon.Where(p => p.VehicleType.Equals(VehicleType.Motorcykel)).Count();
//            return (statistikModell);
//        }
//    }
//}