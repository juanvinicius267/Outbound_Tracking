using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;



namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Controllers
{
    public class MaritimosController : Controller
    {
        private readonly OutboundTrackNTraceContext _context;
        
        public static DateTime Now { get; }

        public MaritimosController(OutboundTrackNTraceContext context)
        {
            _context = context;
        }

        // GET: Maritimos
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            

            var maritimos = from m in _context.Maritimo
                            select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                maritimos = maritimos.Where(s => s.Chassis.Contains(searchString) || s.BatchId.Contains(searchString));
                return View(maritimos);
            }
            else
            {
                return View(await _context.Maritimo.ToListAsync());
            }
            
        }

        // GET: Maritimos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritimo = await _context.Maritimo
                .FirstOrDefaultAsync(m => m.id == id);
            if (maritimo == null)
            {
                return NotFound();
            }

            return View(maritimo);
        }

        // GET: Maritimos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maritimos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,BatchId,PopId,Chassis,CustomerOrder,PartPeriod,Type,Market,Model,PDD,PlanPacking,PlanDelivery,Liner,PortDestination,InttraNumber,Booking,Terminal,Container40,Container20,Vessel,LastDateOutSLA,ETDSantos,ETD2Santos,ATDSantos,ETADestination,ETA2Destination,ATADestination")]  Maritimos maritimo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maritimo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maritimo);
        }

        // GET: Maritimos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritimo = await _context.Maritimo.FindAsync(id);
            if (maritimo == null)
            {
                return NotFound();
            }
            return View(maritimo);
        }

        // POST: Maritimos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,BatchId,PopId,Chassis,CustomerOrder,PartPeriod,Type,Market,Model,PDD,PlanPacking,PlanDelivery,Liner,PortDestination,InttraNumber,Booking,Terminal,Container40,Container20,Vessel,LastDateOutSLA,ETDSantos,ETD2Santos,ATDSantos,ETADestination,ETA2Destination,ATADestination")] Maritimos maritimo)
        {
            if (id != maritimo.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maritimo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaritimoExists(maritimo.id))
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
            return View(maritimo);
        }

        // GET: Maritimos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maritimo = await _context.Maritimo
                .FirstOrDefaultAsync(m => m.id == id);
            if (maritimo == null)
            {
                return NotFound();
            }

            return View(maritimo);
        }

        // POST: Maritimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maritimo = await _context.Maritimo.FindAsync(id);
            _context.Maritimo.Remove(maritimo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
       
       
      
        private bool MaritimoExists(int id)
        {
            return _context.Maritimo.Any(e => e.id == id);
        }
       

      
        
    }
}
