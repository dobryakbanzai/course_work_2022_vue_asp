using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CourseServer;
using Microsoft.AspNetCore.Cors;

namespace CourseServer.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudTeachesController : ControllerBase
    {
        private readonly Kurs_2022Context _context;

        public StudTeachesController(Kurs_2022Context context)
        {
            _context = context;
        }

        // GET: api/StudTeaches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudTeach>>> GetStudTeach()
        {
            return await _context.StudTeach.ToListAsync();
        }

        // GET: api/StudTeaches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudTeach>> GetStudTeach(int id)
        {
            var studTeach = await _context.StudTeach.FindAsync(id);

            if (studTeach == null)
            {
                return NotFound();
            }

            return studTeach;
        }

        // PUT: api/StudTeaches/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudTeach(int id, StudTeach studTeach)
        {
            if (id != studTeach.Id)
            {
                return BadRequest();
            }

            _context.Entry(studTeach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudTeachExists(id))
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

        // POST: api/StudTeaches
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StudTeach>> PostStudTeach(StudTeach studTeach)
        {
            _context.StudTeach.Add(studTeach);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudTeach", new { id = studTeach.Id }, studTeach);
        }

        // DELETE: api/StudTeaches/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudTeach>> DeleteStudTeach(int id)
        {
            var studTeach = await _context.StudTeach.FindAsync(id);
            if (studTeach == null)
            {
                return NotFound();
            }

            _context.StudTeach.Remove(studTeach);
            await _context.SaveChangesAsync();

            return studTeach;
        }

        private bool StudTeachExists(int id)
        {
            return _context.StudTeach.Any(e => e.Id == id);
        }
    }
}
