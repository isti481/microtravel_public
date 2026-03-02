using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microtravel.Data;
using Microtravel.Migrations;
using Microtravel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Microtravel.Controllers
{
    public class TravelsController : Controller
    {
        private readonly MicrotravelContext _context;

        private readonly IWebHostEnvironment _env;

        public TravelsController(MicrotravelContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        private static string GenerateUniqueId()
        {
            // GUID, majd az első 10 karaktert használjuk
            return Guid.NewGuid().ToString("N").Substring(0, 10);
        }

        // GET: Travels
        public async Task<IActionResult> Index()
        {
            var microtravelContext1 = await _context.Travel.Include(t => t.TravelDealType).ToListAsync();

            var microtravelContext2 = await _context.Travel.Include(t => t.TravelType).ToListAsync();
            
            var microtravelContext3 = await _context.Travel.ToListAsync();
            
            var microtravelContext = _context.Travel
                .Include(t => t.TravelDealType)
                .Include(t => t.TravelType);
            return View(await microtravelContext.ToListAsync());
        }

        public async Task<IActionResult> Travel()
        {

            var microtravelContext = _context.Travel
                .Include(t => t.TravelDealType)
                .Include(t => t.TravelType)
                .Where(t => t.Enabled == 1);
            return View(await microtravelContext.ToListAsync());
        }

        // GET: Travels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var travel = await _context.Travel
                .Include(t => t.TravelDealType)
                .Include(t => t.TravelType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (travel == null)
            {
                return NotFound();
            }

        

            return View(travel);
            
            //
            //if (travel == null)
            //{
            //    return NotFound();
            //}
            //

            //ViewData["TravelDealTypeId"] = new SelectList(_context.Set<TravelDealType>(), "Id", "Name");
            //ViewData["TravelTypeId"] = new SelectList(_context.Set<TravelType>(), "Id", "Name");
            //return View();
        }

        // GET: Travels/Create
        public IActionResult Create()
        {
            ViewData["TravelDealTypeId"] = new SelectList(_context.Set<TravelDealType>(), "Id", "Name");
            ViewData["TravelTypeId"] = new SelectList(_context.Set<TravelType>(), "Id", "Name");
            return View();
        }

        // POST: Travels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TravelTypeId,TravelDealTypeId,Description,Price,travelPictureUrl,TravelDate,Enabled,TravelIdentifier")] Travel travel, IFormFile file)
        {

            #region hibakeresés
            //if (!ModelState.IsValid)
            //{
            //    foreach (var item in ModelState)
            //    {
            //        foreach (var error in item.Value.Errors)
            //        {
            //            Console.WriteLine($"{item.Key} - {error.ErrorMessage}");
            //        }
            //    }
            //}

            // hibakeresés

            //if (!ModelState.IsValid)
            //{
            //    foreach (var state in ModelState)
            //    {
            //        foreach (var error in state.Value.Errors)
            //        {
            //            System.Diagnostics.Debug.WriteLine(
            //                $"FIELD: {state.Key} ERROR: {error.ErrorMessage}");
            //        }
            //    }
            //}

            #endregion
            if (ModelState.IsValid)
            {
                travel.TravelRegDate = DateTime.Now;
                travel.Enabled = 0;
                travel.TravelIdentifier = GenerateUniqueId();

                //

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(_env.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    travel.travelPictureUrl = "/images/" + fileName;
                }

                //


                _context.Add(travel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TravelDealTypeId"] = new SelectList(_context.Set<TravelDealType>(), "Id", "Name", travel.TravelDealTypeId);
            ViewData["TravelTypeId"] = new SelectList(_context.Set<TravelType>(), "Id", "Name", travel.TravelTypeId);
            return View(travel);
        }

        // GET: Travels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travel = await _context.Travel.FindAsync(id);
            if (travel == null)
            {
                return NotFound();
            }
            ViewData["TravelDealTypeId"] = new SelectList(_context.Set<TravelDealType>(), "Id", "Name", travel.TravelDealTypeId);
            ViewData["TravelTypeId"] = new SelectList(_context.Set<TravelType>(), "Id", "Name", travel.TravelTypeId);
            return View(travel);
        }

        // POST: Travels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TravelTypeId,TravelDealTypeId,Description,Price,travelPictureUrl,TravelDate,Enabled")] Travel travel, IFormFile? file)
        {

            //tracking hiba
            //_context.ChangeTracker.Clear();


            if (id != travel.Id)
            {
                return NotFound();
            }

            

            if (ModelState.IsValid)
            {
                try
                {



                    if (file != null && file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(_env.WebRootPath, "images", fileName);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        travel.travelPictureUrl = "/images/" + fileName;

                        // ha képet nem adok hozzá 
                    }
                    else
                    {
                        //var travelSorted = await _context.Travel.FindAsync(id);
                        var travelSorted = await _context.Travel
                                            .Where(t => t.Id == id)
                                            .Select(t => t.travelPictureUrl)
                                            .FirstOrDefaultAsync();

                        travel.travelPictureUrl = travelSorted;
                    }



                    _context.Update(travel);
                    _context.Entry(travel).Property(x => x.TravelRegDate).IsModified = false;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelExists(travel.Id))
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
            ViewData["TravelDealTypeId"] = new SelectList(_context.Set<TravelDealType>(), "Id", "Id", travel.TravelDealTypeId);
            ViewData["TravelTypeId"] = new SelectList(_context.Set<TravelType>(), "Id", "Id", travel.TravelTypeId);
            return View(travel);
        }

        // GET: Travels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travel = await _context.Travel
                .Include(t => t.TravelDealType)
                .Include(t => t.TravelType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travel == null)
            {
                return NotFound();
            }

            return View(travel);
        }

        // POST: Travels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travel = await _context.Travel.FindAsync(id);
            if (travel != null)
            {
                _context.Travel.Remove(travel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelExists(int id)
        {
            return _context.Travel.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Search(string searchstring)
        {
            var data = await _context.Travel
                .Where(t => string.IsNullOrEmpty(searchstring)
                || t.Name.Contains(searchstring)
                || t.TravelDealType.Name.StartsWith(searchstring))
                .Include(t => t.TravelDealType)
                .Include(t => t.TravelType)
                .ToListAsync();

            return PartialView("_TravelCardRender", data);
        }

        public async Task<IActionResult> ResetDatabase()
        {
            await _context.Database.ExecuteSqlRawAsync("EXEC StpResetDatabase");

            return RedirectToAction("Index");
        }


    }
}
