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
    public class ChallengesController : ControllerBase
    {
        private readonly Kurs_2022Context _context;

        public ChallengesController(Kurs_2022Context context)
        {
            _context = context;
        }

        // GET: api/Challenges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Challenges>>> GetChallenges()
        {
            return await _context.Challenges.ToListAsync();
        }

        // GET: api/Challenges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Challenges>> GetChallenges(int id)
        {
            var challenges = await _context.Challenges.FindAsync(id);

            if (challenges == null)
            {
                return NotFound();
            }

            return challenges;
        }

        // PUT: api/Challenges/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChallenges(int id, Challenges challenges)
        {
            if (id != challenges.Id)
            {
                return BadRequest();
            }

            _context.Entry(challenges).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengesExists(id))
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

        // POST: api/Challenges
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Challenges>> PostChallenges(Challenges challenges)
        {
            _context.Challenges.Add(challenges);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetChallenges", new { id = challenges.Id }, challenges);
            //return CreatedAtAction(nameof(GetChallenges), new { id = challenges.Id }, challenges);
            return NoContent();
        }

        // DELETE: api/Challenges/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Challenges>> DeleteChallenges(int id)
        {
            var challenges = await _context.Challenges.FindAsync(id);
            if (challenges == null)
            {
                return NotFound();
            }

            _context.Challenges.Remove(challenges);
            await _context.SaveChangesAsync();

            return challenges;
        }

        private bool ChallengesExists(int id)
        {
            return _context.Challenges.Any(e => e.Id == id);
        }
    }
}
