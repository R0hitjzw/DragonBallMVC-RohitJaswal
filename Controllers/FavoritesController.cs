using DragonBallMVC.Data;
using DragonBallMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DragonBallMVC.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _db;

        public FavoritesController(AppDbContext db)
        {
            _db = db;
        }

        // GET /Favorites
        public async Task<IActionResult> Index()
        {
            var favorites = await _db.Favorites.OrderByDescending(f => f.AddedAt).ToListAsync();
            return View(favorites);
        }

        // POST /Favorites/Add
        [HttpPost]
        public async Task<IActionResult> Add(int characterId, string characterName, string characterImage, string characterRace)
        {
            bool exists = await _db.Favorites.AnyAsync(f => f.CharacterId == characterId);
            if (!exists)
            {
                _db.Favorites.Add(new Favorite
                {
                    CharacterId = characterId,
                    CharacterName = characterName,
                    CharacterImage = characterImage,
                    CharacterRace = characterRace
                });
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Detail", "Characters", new { id = characterId });
        }

        // POST /Favorites/Remove/5
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var fav = await _db.Favorites.FindAsync(id);
            if (fav != null)
            {
                _db.Favorites.Remove(fav);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}