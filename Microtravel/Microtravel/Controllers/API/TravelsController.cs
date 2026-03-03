using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microtravel.Data;
using Microtravel.Models;
using NuGet.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microtravel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelsController : ControllerBase
    {
        private readonly MicrotravelContext _context;

        public TravelsController(MicrotravelContext context)
        {
            _context = context;
        }


        // GET: api/Travels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Travel>>> GetTravel([FromHeader] string token)
        {
            var apiToken = await _context.Apiuser.FirstOrDefaultAsync(t => t.ApiToken == token);
     

            if (token != apiToken.ApiToken)
            {
                return Unauthorized();
            }

            //return await _context.Travel.ToListAsync();
            return await _context.Travel
                .Include(t => t.TravelDealType)
                .Include(t => t.TravelType)                
                .ToListAsync();
        }

        // GET: api/Travels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Travel>> GetTravel(int id, [FromHeader] string token)
        {
            var apiToken = await _context.Apiuser.FirstOrDefaultAsync(t => t.ApiToken == token);

            if (token != apiToken.ApiToken)
            {
                return Unauthorized();
            }

            var travel = await _context.Travel.FindAsync(id);

            if (travel == null)
            {
                return NotFound();
            }

            return travel;
        }

        // PUT: api/Travels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTravel(int id,[FromBody] Travel travel, [FromHeader] string token)
        {
            var apiToken = await _context.Apiuser.FirstOrDefaultAsync(t => t.ApiToken == token);

            if (token != apiToken.ApiToken)
            {
                return Unauthorized();
            }

            // check the POST_booking key
            if (apiToken.Name != "POST_booking")
            {
                return Unauthorized();
            }

            if (id != travel.Id)
            {
                return BadRequest();
            }

            /*---------------------------------------------------------------------------*/
            //_context.Entry(travel).State = EntityState.Modified;
            /*---------------------------------------------------------------------------*/

            var existingTravel = await _context.Travel.FindAsync(id);
            if (existingTravel == null) return NotFound();

            // Csak azokat a mezőket frissíted, amiket a front küldött
            existingTravel.Name = travel.Name;
            existingTravel.Description = travel.Description;
            existingTravel.Price = travel.Price;
            existingTravel.TravelDate = travel.TravelDate;
            existingTravel.TravelTypeId = travel.TravelTypeId;
            existingTravel.TravelDealTypeId = travel.TravelDealTypeId;
            existingTravel.Enabled = travel.Enabled;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Travels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Travel>> PostTravel([FromBody]  Travel travel, [FromHeader] string token)
        {
            var apiToken = await _context.Apiuser.FirstOrDefaultAsync(t => t.ApiToken == token);

            if (token != apiToken.ApiToken)
            {
                return Unauthorized();
            }

            // check the POST_booking key
            if (apiToken.Name != "POST_booking")
            {
                return Unauthorized();
            }

            _context.Travel.Add(travel);
            travel.TravelRegDate = DateTime.Now;
            travel.Enabled = 0;
            await _context.SaveChangesAsync();


            /*-----------------------------------------------------------------------*/
            //var travelWithIncludes = await _context.Travel
            //.Include(t => t.TravelType)
            //.Include(t => t.TravelDealType)
            //.FirstOrDefaultAsync(t => t.Id == travel.Id);

            //return CreatedAtAction("GetTravel", new { id = travel.Id }, travel);
            /*-----------------------------------------------------------------------*/

            var travelDto = await _context.Travel
                .Where(t => t.Id == travel.Id)
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.TravelTypeId,
                    t.TravelDealTypeId,
                    t.Description,
                    t.Price,
                    t.travelPictureUrl,
                    t.TravelDate,
                    t.TravelRegDate,
                    t.Enabled
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction("GetTravel", new { id = travel.Id }, travelDto);
        }

        // DELETE: api/Travels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravel(int id, [FromHeader] string token)
        {
            var apiToken = await _context.Apiuser.FirstOrDefaultAsync(t => t.ApiToken == token);

            if (token != apiToken.ApiToken)
            {
                return Unauthorized();
            }

            // check the POST_booking key
            if (apiToken.Name != "POST_booking")
            {
                return Unauthorized();
            }

            var travel = await _context.Travel.FindAsync(id);
            if (travel == null)
            {
                return NotFound();
            }

            _context.Travel.Remove(travel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TravelExists(int id)
        {
            return _context.Travel.Any(e => e.Id == id);
        }
    }
}
