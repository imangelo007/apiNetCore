using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AController : ControllerBase
    {
        private readonly abcContext _context;

        public AController(abcContext context)
        {
            _context = context;
        }

        // GET: api/A
        [HttpGet]
        public IEnumerable<A> GetA()
        {
            return _context.A;
        }

        // GET: api/A/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetA([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var a = await _context.A.FindAsync(id);

            if (a == null)
            {
                return NotFound();
            }

            return Ok(a);
        }

        // PUT: api/A/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutA([FromRoute] int id, [FromBody] A a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != a.Id)
            {
                return BadRequest();
            }

            _context.Entry(a).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AExists(id))
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

        // POST: api/A
        [HttpPost]
        public async Task<IActionResult> PostA([FromBody] A a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.A.Add(a);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetA", new { id = a.Id }, a);
        }

        // DELETE: api/A/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteA([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var a = await _context.A.FindAsync(id);
            if (a == null)
            {
                return NotFound();
            }

            _context.A.Remove(a);
            await _context.SaveChangesAsync();

            return Ok(a);
        }

        private bool AExists(int id)
        {
            return _context.A.Any(e => e.Id == id);
        }
    }
}