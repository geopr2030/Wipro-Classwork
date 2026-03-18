using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Models;

namespace DriveEase.Controllers
{
    public class LeasesController : Controller
    {
        private readonly DriveEaseContext _context;

        public LeasesController(DriveEaseContext context)
        {
            _context = context;
        }

        // GET: Leases
        public async Task<IActionResult> Index()
        {
            var driveEaseContext = _context.Leases.Include(l => l.Customer).Include(l => l.Vehicle);
            return View(await driveEaseContext.ToListAsync());
        }

        // GET: Leases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lease = await _context.Leases
                .Include(l => l.Customer)
                .Include(l => l.Vehicle)
                .FirstOrDefaultAsync(m => m.LeaseId == id);
            if (lease == null)
            {
                return NotFound();
            }

            return View(lease);
        }

        // GET: Leases/Create
        public IActionResult Create()
        {
            var availableVehicles = _context.Vehicles.Where(v => v.Status == "Available").ToList();

            ViewData["VehicleId"] = new SelectList(availableVehicles, "VehicleId", "Make");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName");

            return View();
        }

        // POST: Leases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //adding actual Car Rental logic 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaseId,VehicleId,CustomerId,StartDate,EndDate,Type")] Lease lease)
        {
            // Reload dropdowns always (so view works even on validation errors)
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", lease.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "Make", lease.VehicleId);
            if(lease.EndDate<=lease.StartDate)
            {
                ModelState.AddModelError("", "End date must be after start date.");
                return View(lease);
            }

            if (!ModelState.IsValid)
                return View(lease);

            var vehicle = await _context.Vehicles.FindAsync(lease.VehicleId);
            if (vehicle == null)
            {
                ModelState.AddModelError("", "Vehicle not found.");
                return View(lease);
            }

            if (vehicle.Status != "Available")
            {
                ModelState.AddModelError("", "Vehicle is already rented.");
                return View(lease);
            }

            // Mark car as rented
            vehicle.Status = "Rented";

           

            _context.Leases.Add(lease);
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Leases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lease = await _context.Leases.FindAsync(id);
            if (lease == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", lease.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "EngineCapacity", lease.VehicleId);
            return View(lease);
        }

        // POST: Leases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaseId,VehicleId,CustomerId,StartDate,EndDate,Type")] Lease lease)
        {
            if (id != lease.LeaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lease);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaseExists(lease.LeaseId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Email", lease.CustomerId);
            ViewData["VehicleId"] = new SelectList(_context.Vehicles, "VehicleId", "EngineCapacity", lease.VehicleId);
            return View(lease);
        }

        // GET: Leases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lease = await _context.Leases
                .Include(l => l.Customer)
                .Include(l => l.Vehicle)
                .FirstOrDefaultAsync(m => m.LeaseId == id);
            if (lease == null)
            {
                return NotFound();
            }

            return View(lease);
        }

        // POST: Leases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lease = await _context.Leases.FindAsync(id);
            if (lease != null)
            {
                _context.Leases.Remove(lease);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Adding Action for return
        // GET: Leases/Return/5
        public async Task<IActionResult> Return(int? id)
        {
            if (id == null) return NotFound();

            var lease = await _context.Leases
                .Include(l => l.Vehicle)
                .Include(l => l.Customer)
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null) return NotFound();

            return View(lease); // will open Views/Leases/Return.cshtml
        }

        // POST: Leases/Return/5
        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnConfirmed(int id)
        {
            var lease = await _context.Leases
                .Include(l => l.Vehicle)
                .FirstOrDefaultAsync(l => l.LeaseId == id);

            if (lease == null) return NotFound();

            // ✅ Vehicle available again
            if (lease.Vehicle != null)
            {
                lease.Vehicle.Status = "Available";
                _context.Vehicles.Update(lease.Vehicle);
            }

            // Optional: set actual return time
            // lease.EndDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Creating active rental page that shows : 1)which car is out 2)who rented it  3)From when  4)When it should return
        public async Task<IActionResult> Active()
        {
            var activeLeases = await _context.Leases
                .Include(l => l.Vehicle)
                .Include(l => l.Customer)
                .Where(l => l.Vehicle.Status == "Rented")
                .ToListAsync();

            return View(activeLeases);
        }
        private bool LeaseExists(int id)
        {
            return _context.Leases.Any(e => e.LeaseId == id);
        }
    }
}
