using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Configuracao;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeofencingController : ControllerBase
    {
        private readonly Contexto _context;

        public GeofencingController(Contexto context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Geofencing>>> GetGeofencings()
        {
            var geofencings = await _context.Geofencing.ToListAsync();

            if (geofencings == null || geofencings.Count == 0)
            {
                return NotFound();
            }

            return geofencings;
        }

        [HttpPost]
        public async Task<ActionResult<Geofencing>> PostGeofencing(Geofencing geofencing)
        {
            if (_context.Geofencing == null)
            {
                return Problem("Entity set 'Contexto.Geofencing'  is null.");
            }
            _context.Geofencing.Add(geofencing);
            await _context.SaveChangesAsync();

            return geofencing;
        }
    }
}
