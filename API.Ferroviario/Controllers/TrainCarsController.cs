using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos.Ferroviario.Modelos;

namespace API.Ferroviario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainCarsController : ControllerBase
    {
        private readonly APIFerroviarioContext _context;

        public TrainCarsController(APIFerroviarioContext context)
        {
            _context = context;
        }

        // GET: api/TrainCars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainCar>>> GetTrainCar()
        {
            return await _context.TrainsCars.ToListAsync();
        }

        [HttpGet("Train/{id}")]
        public async Task<ActionResult<IEnumerable<TrainCar>>> GetVagonesByTrain(int id)
        {
            var data = await _context.TrainsCars
                .Where(v => v.TrenId == id)
                .Include(e => e.Tren)
                .ToListAsync();

            return data;
        }

        // GET: api/TrainCars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainCar>> GetTrainCar(int id)
        {
            var trainCar = await _context.TrainsCars.FindAsync(id);

            if (trainCar == null)
            {
                return NotFound();
            }

            return trainCar;
        }

        // PUT: api/TrainCars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainCar(int id, TrainCar trainCar)
        {
            if (id != trainCar.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainCar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainCarExists(id))
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

        // POST: api/TrainCars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrainCar>> PostTrainCar(TrainCar trainCar)
        {
            _context.TrainsCars.Add(trainCar);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainCar", new { id = trainCar.Id }, trainCar);
        }

        // DELETE: api/TrainCars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainCar(int id)
        {
            var trainCar = await _context.TrainsCars.FindAsync(id);
            if (trainCar == null)
            {
                return NotFound();
            }

            _context.TrainsCars.Remove(trainCar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainCarExists(int id)
        {
            return _context.TrainsCars.Any(e => e.Id == id);
        }
    }
}
