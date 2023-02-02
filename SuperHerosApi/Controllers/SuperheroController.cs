using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace SuperHerosApi.Controllers;

// async function without await performed syncroniously and with await it's performe asyncroniously.

[Route("api/[controller]")]
[ApiController]
public class SuperheroController : ControllerBase
{

    private static List<Superhero> heros = new List<Superhero>
        {
           new Superhero
           {
               Id = 1 ,
               Name = "Spider-Man",
               FirstName = "Piter",
               LastName = "Parcor",
               Place = "New Yock City"
           },

           new Superhero
           {
               Id = 2 , 
               Name = "Ironman",
               FirstName = "Tony",
               LastName = "Starck",
               Place = "Long Island"
           }

        };
    private readonly DataAccessClass _context;
    public SuperheroController(DataAccessClass Context)
    {
        _context = Context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Superhero>>> Get()
    {
        return Ok(await _context.superheros.ToListAsync());
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<List<Superhero>>> Get(int id)
    {
        var hero = await _context.superheros.FindAsync(id);
        if(hero is null)
        {
            return BadRequest("Id was not define");
        }
        else
        {
            return Ok(hero);
        }
    }

    [HttpPut]
    public async Task<ActionResult<List<Superhero>>> Put(Superhero Request)
    {
        var Dbhero = await _context.superheros.FindAsync(Request.Id);
        if(Dbhero is null)
        {
            return BadRequest("Id was not define in list");
        }
        else
        {
            Dbhero.Name = Request.Name;
            Dbhero.FirstName = Request.FirstName;
            Dbhero.LastName = Request.LastName;
            Dbhero.Place = Request.Place;
        }
        await _context.SaveChangesAsync();
        return Ok(await _context.superheros.ToListAsync());
    }


    [HttpPost]
    public async Task<ActionResult<List<Superhero>>> Post(Superhero hero)
    {
        _context.superheros.Add(hero);
        await _context.SaveChangesAsync();
        return Ok(await _context.superheros.ToListAsync());
    }



    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Superhero>>> Delete(int id)
    {
        var Dbhero = await _context.superheros.FindAsync(id);
        if(Dbhero is null)
        {
            return BadRequest("Not found");
        }
        else
        {
             _context.superheros.Remove(Dbhero);
            await _context.SaveChangesAsync();
        }

        return Ok(await _context.superheros.ToListAsync());
    }
} 