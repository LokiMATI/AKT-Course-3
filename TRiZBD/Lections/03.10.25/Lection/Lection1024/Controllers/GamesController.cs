using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lection1024.Contexts;
using Lection1024.Models;
using Lection1024.DTOs;

namespace Lection1024.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(GamesDbContext context) : ControllerBase
    {
        private readonly GamesDbContext _context = context;

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            return await _context.Games.ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        #region get-methods

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByCategories([FromQuery] string categories)
        {
            var values = categories.Split(',', StringSplitOptions.TrimEntries|StringSplitOptions.RemoveEmptyEntries);

            return await _context.Games
                .Include(g => g.Category)
                .Where(g => values.Contains(g.Name))
                .ToListAsync();
        }

        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByCategory([FromQuery] string name)
        {
            var category = await _context.Categories
                .Include(c => c.Games)
                .FirstOrDefaultAsync(c => c.Name == name);

            return category.Games.ToList();
        }

        [HttpGet("price")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByPrice([FromQuery] string price)
        {
            var values = price.Split(',');

            if (values.Length != 2)
                return BadRequest();

            int minPrice, maxPrice;
            if (!int.TryParse(values[0], out minPrice) ||
                !int.TryParse(values[1], out maxPrice))
                return BadRequest();

            return await _context.Games
                .Include(g => g.Category)
                .Where(g => g.Price >= minPrice && g.Price <= maxPrice)
                .ToListAsync();
        }

        [HttpGet("info")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesInfo()
        {
            return await _context.Games
                .Select(g => g.ToDto())
                .ToListAsync();
        }

        #endregion

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.GameId)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGame", new { id = game.GameId }, game);
        }

        // DELETE: api/Games/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.GameId == id);
        }
    }
}
