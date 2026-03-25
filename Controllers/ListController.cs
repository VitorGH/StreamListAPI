using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StreamListAPI.Data;
using StreamListAPI.Models;
using System.Security.Claims;
using StreamListAPI.DTOs.Watchlist;

namespace StreamListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ListController(AppDbContext db) : ControllerBase
    {
        private int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await db.WatchlistItems
                .Where(w => w.UserId == UserId)
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> Add(WatchlistItemDto dto)
        {
            var item = new WatchlistItem
            {
                UserId = UserId,
                TmdbId = dto.TmdbId,
                MediaType = dto.MediaType,
                Title = dto.Title,
                PosterPath = dto.PosterPath,
                Status = dto.Status
            };
            db.WatchlistItems.Add(item);
            await db.SaveChangesAsync();
            return Ok(item);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, UpdateItemDto dto)
        {
            var item = await db.WatchlistItems.FirstOrDefaultAsync(w => w.Id == id && w.UserId == UserId);
            if (item is null) return NotFound();
            if (dto.Status is not null) item.Status = dto.Status;
            if (dto.Rating.HasValue) item.Rating = dto.Rating;
            if (dto.Notes is not null) item.Notes = dto.Notes;
            await db.SaveChangesAsync();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await db.WatchlistItems.FirstOrDefaultAsync(w => w.Id == id && w.UserId == UserId);
            if (item is null) return NotFound();
            db.WatchlistItems.Remove(item);
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}
