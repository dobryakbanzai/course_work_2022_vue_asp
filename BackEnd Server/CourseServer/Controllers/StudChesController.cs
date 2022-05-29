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
    public class StudChesController : ControllerBase
    {
        private readonly Kurs_2022Context _context;

        public StudChesController(Kurs_2022Context context)
        {
            _context = context;
        }

        // GET: api/StudChes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudCh>>> GetStudCh()
        {
            var stch = await _context.StudCh.ToListAsync();
            var stch1 = new List<StudCh>();
            foreach (var item in stch)
            {
                stch1.Add(new StudCh 
                {
                    Id = item.Id,
                    ChId = item.ChId,
                    StudId = item.StudId,
                    StudChScore = item.StudChScore,
                    Ch = await _context.Challenges.FindAsync(item.ChId),
                    Stud = await _context.Students.FindAsync(item.StudId)
                });
            }
            foreach (var item in stch1)
            {
                item.Ch.StudCh = null;
                item.Stud.StudCh = null;

            }
            return await _context.StudCh.ToListAsync();
        }

        // GET: api/StudChes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudCh>> GetStudCh(int id)
        {
            var studCh = await _context.StudCh.FindAsync(id);

            if (studCh == null)
            {
                return NotFound();
            }

            return studCh;
        }

        // PUT: api/StudChes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudCh(int id, StudCh studCh)
        {
            if (id != studCh.Id)
            {
                return BadRequest();
            }

            _context.Entry(studCh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudChExists(id))
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

        // POST: api/StudChes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StudCh>> PostStudCh(StudCh studCh)
        {
            _context.StudCh.Add(studCh);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudCh", new { id = studCh.Id }, studCh);
        }

        // DELETE: api/StudChes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudCh>> DeleteStudCh(int id)
        {
            var studCh = await _context.StudCh.FindAsync(id);
            if (studCh == null)
            {
                return NotFound();
            }

            _context.StudCh.Remove(studCh);
            await _context.SaveChangesAsync();

            return studCh;
        }

        private bool StudChExists(int id)
        {
            return _context.StudCh.Any(e => e.Id == id);
        }
    }
}
