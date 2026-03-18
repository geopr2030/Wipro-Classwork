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
    public class PaymentsController : Controller
    {
        private readonly DriveEaseContext _context;

        public PaymentsController(DriveEaseContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var driveEaseContext = _context.Payments.Include(p => p.Lease);
            return View(await driveEaseContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Lease)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public async Task<IActionResult> Create()
        {
            var paidLeaseIds = await _context.Payments
    .Select(p => p.LeaseId)
    .ToListAsync();

            var unpaidLeases = await _context.Leases
                .Include(l => l.Vehicle)
                .Include(l => l.Customer)
                .Where(l => !paidLeaseIds.Contains(l.LeaseId))
                .ToListAsync();

            var leaseDropdown = unpaidLeases.Select(l => new
            {
                l.LeaseId,
                Display = $"{l.LeaseId} - {l.Vehicle.Make} {l.Vehicle.Model} | {l.Customer.Email} | {l.StartDate:dd-MMM} → {l.EndDate:dd-MMM} | {l.Type}"
            }).ToList();

            ViewData["LeaseId"] = new SelectList(leaseDropdown, "LeaseId", "Display");

            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,LeaseId")] Payment payment)
        {
            // reload dropdown in case of errors
            ViewData["LeaseId"] = new SelectList(_context.Leases, "LeaseId", "LeaseId", payment.LeaseId);

            // get lease with vehicle
            var lease = await _context.Leases
                .Include(l => l.Vehicle)
                .FirstOrDefaultAsync(l => l.LeaseId == payment.LeaseId);

            if (lease == null)
            {
                ModelState.AddModelError("", "Lease not found.");
                return View(payment);
            }

            if (lease.Vehicle == null)
            {
                ModelState.AddModelError("", "Vehicle not found for this lease.");
                return View(payment);
            }

            // prevent duplicate payment (optional)
            var alreadyPaid = await _context.Payments.AnyAsync(p => p.LeaseId == payment.LeaseId);
            if (alreadyPaid)
            {
                ModelState.AddModelError("", "Payment already exists for this lease.");
                return View(payment);
            }

            // calculate days
            var days = (lease.EndDate.Date - lease.StartDate.Date).Days;
            if (days <= 0) days = 1;

            // calculate amount
            if (!string.IsNullOrWhiteSpace(lease.Type) && lease.Type.ToLower() == "monthly")
            {
                var months = (int)Math.Ceiling(days / 30.0);
                payment.Amount = months * lease.Vehicle.DailyRate * 30;
            }
            else
            {
                payment.Amount = days * lease.Vehicle.DailyRate;
            }

            payment.PaymentDate = DateTime.Now;

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["LeaseId"] = new SelectList(_context.Leases, "LeaseId", "Type", payment.LeaseId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,LeaseId,Amount,PaymentDate")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["LeaseId"] = new SelectList(_context.Leases, "LeaseId", "Type", payment.LeaseId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Lease)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Adding  total revenue report page
        public async Task<IActionResult> RevenueReport()
        {
            // Total revenue from payments
            var totalRevenue = await _context.Payments.SumAsync(p => (decimal?)p.Amount) ?? 0m;

            // Total rentals from leases (all leases created)
            var totalRentals = await _context.Leases.CountAsync();

            // Avg payment from payments
            var avgPayment = await _context.Payments.AverageAsync(p => (decimal?)p.Amount) ?? 0m;

            // Bonus metrics
            var today = DateTime.Today;
            var revenueToday = await _context.Payments
                .Where(p => p.PaymentDate.Date == today)
                .SumAsync(p => (decimal?)p.Amount) ?? 0m;

            var firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var revenueThisMonth = await _context.Payments
                .Where(p => p.PaymentDate >= firstDayOfMonth)
                .SumAsync(p => (decimal?)p.Amount) ?? 0m;

            var paymentsCount = await _context.Payments.CountAsync();

            var vm = new RevenueReportViewModel
            {
                TotalRevenue = totalRevenue,
                TotalRentals = totalRentals,
                AveragePayment = avgPayment,
                RevenueToday = revenueToday,
                RevenueThisMonth = revenueThisMonth,
                PaymentsCount = paymentsCount
            };

            return View(vm);
        }
        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.PaymentId == id);
        }
    }
}
