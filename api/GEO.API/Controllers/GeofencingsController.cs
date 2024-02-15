using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Configuracao;
using api.Models;

namespace GEO.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeofencingsController : ControllerBase
    {
        private readonly Contexto _context;

        public GeofencingsController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Geofencings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Geofencing>>> GetGeofencing()
        {
            return await _context.Geofencing.ToListAsync();
        }

        // GET: api/Geofencings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Geofencing>> GetGeofencing(DateTime id)
        {
            var geofencing = await _context.Geofencing.FindAsync(id);

            if (geofencing == null)
            {
                return NotFound();
            }

            return geofencing;
        }

        // PUT: api/Geofencings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeofencing(DateTime id, Geofencing geofencing)
        {
            if (id != geofencing.horario)
            {
                return BadRequest();
            }

            _context.Entry(geofencing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeofencingExists(id))
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

        // POST: api/Geofencings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Geofencing>> PostGeofencing(Geofencing geofencing)
        {
            _context.Geofencing.Add(geofencing);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GeofencingExists(geofencing.horario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGeofencing", new { id = geofencing.horario }, geofencing);
        }

        // DELETE: api/Geofencings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeofencing(DateTime id)
        {
            var geofencing = await _context.Geofencing.FindAsync(id);
            if (geofencing == null)
            {
                return NotFound();
            }

            _context.Geofencing.Remove(geofencing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeofencingExists(DateTime id)
        {
            return _context.Geofencing.Any(e => e.horario == id);
        }
    }
}
