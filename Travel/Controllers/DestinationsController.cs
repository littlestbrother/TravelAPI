using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Travel.Models;
using System.Linq;
using System;

namespace Travel.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DestinationsController : ControllerBase
  {
    private readonly TravelContext _db;

    public DestinationsController(TravelContext db)
    {
      _db = db;
    }

    // GET: api/Destinations
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Destination>>> Get(string city, string state, string rating, string country, string visitDate)
    {
      var query = _db.Destinations.AsQueryable();

      if (city != null)
      {
        query = query.Where(entry => entry.City == city);
      }

      if (state != null)
      {
        query = query.Where(entry => entry.State == state);
      }

      if (rating != null)
      {
        query = query.Where(entry => entry.Rating == rating);
      }

      if (country != null)
      {
        query = query.Where(entry => entry.Country == country);
      }

      if (visitDate != null)
      {
        query = query.Where(entry => entry.VisitDate == visitDate);
      }

      return await query.OrderByDescending(entry => entry.Rating).ToListAsync();
    }
    
    // POST api/destinations
    [HttpPost]
    public async Task<ActionResult<Destination>> Post(Destination destination)
    {
      _db.Destinations.Add(destination);
      await _db.SaveChangesAsync();

     return CreatedAtAction(nameof(GetDestination), new { id = destination.DestinationId }, destination);
    }

    //GET api/Destinations/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetDestination(int id)
    {
        var destination = await _db.Destinations.FindAsync(id);

        if (destination == null)
        {
            return NotFound();
        }

        return destination;
    }

     //GET api/Destinations/random
    [HttpGet("random")]
    public async Task<ActionResult<Destination>> GetDestination(string random)
    {
        int count = _db.Destinations.Count();
        Random rng = new Random();
        int id = rng.Next(1, count);
        var destination = await _db.Destinations.FindAsync(id);
        return destination;
    }
    
    // PUT: api/destinations/user
    [HttpPut("{id}/{userName}")]
    public async Task<IActionResult> Put(int id, Destination destination, string userName)
    {
      if (id != destination.DestinationId || userName != destination.UserName)
      {
        return BadRequest();
      }

      _db.Entry(destination).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!DestinationExists(id))
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
     private bool DestinationExists(int id)
    {
      return _db.Destinations.Any(e => e.DestinationId == id);
    }

    // DELETE: api/destinations/5
    [HttpDelete("{id}/{userName}")]
    public async Task<IActionResult> DeleteDestination(int id, string userName)
    {
      var destination = await _db.Destinations.FindAsync(id);

      if(userName != destination.UserName)
      {
        return BadRequest();
      } else {
      
      if (destination == null)
      {
        return NotFound();
      }

      _db.Destinations.Remove(destination);
      await _db.SaveChangesAsync();

      return NoContent();
      }
    }
  }
}