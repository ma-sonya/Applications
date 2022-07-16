using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DrugstoreAPIWebApp.Models;

namespace DrugstoreAPIWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerDrugsController : ControllerBase
    {
        private readonly DrugstoreAPIContext _context;

        public ProducerDrugsController(DrugstoreAPIContext context)
        {
            _context = context;
        }

        // GET: api/ProducerDrugs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProducerDrug>>> GetProducerDrugs()
        {
          if (_context.ProducerDrugs == null)
          {
              return NotFound();
          }
            return await _context.ProducerDrugs.ToListAsync();
        }

        // GET: api/ProducerDrugs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProducerDrug>> GetProducerDrug(int id)
        {
          if (_context.ProducerDrugs == null)
          {
              return NotFound();
          }
            var producerDrug = await _context.ProducerDrugs.FindAsync(id);

            if (producerDrug == null)
            {
                return NotFound();
            }

            return producerDrug;
        }

        // PUT: api/ProducerDrugs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducerDrug(int id, ProducerDrug producerDrug)
        {
            if (id != producerDrug.Id)
            {
                return BadRequest();
            }

            _context.Entry(producerDrug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProducerDrugExists(id))
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

        // POST: api/ProducerDrugs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProducerDrug>> PostProducerDrug(ProducerDrug producerDrug)
        {
          if (_context.ProducerDrugs == null)
          {
              return Problem("Entity set 'DrugstoreAPIContext.ProducerDrugs'  is null.");
          }
            _context.ProducerDrugs.Add(producerDrug);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducerDrug", new { id = producerDrug.Id }, producerDrug);
        }

        // DELETE: api/ProducerDrugs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducerDrug(int id)
        {
            if (_context.ProducerDrugs == null)
            {
                return NotFound();
            }
            var producerDrug = await _context.ProducerDrugs.FindAsync(id);
            if (producerDrug == null)
            {
                return NotFound();
            }

            _context.ProducerDrugs.Remove(producerDrug);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProducerDrugExists(int id)
        {
            return (_context.ProducerDrugs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
