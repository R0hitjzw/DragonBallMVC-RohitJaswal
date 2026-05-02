using DragonBallMVC.Models;
using DragonBallMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace DragonBallMVC.Controllers
{
    public class CharactersController : Controller
    {
        private readonly DragonBallService _service;

        public CharactersController(DragonBallService service)
        {
            _service = service;
        }

        // GET /Characters o /
        public async Task<IActionResult> Index(int page = 1)
        {
            var response = await _service.GetCharactersAsync(page, 12);
            ViewBag.CurrentPage = response.Meta.CurrentPage;
            ViewBag.TotalPages = response.Meta.TotalPages;
            return View(response.Items);
        }

        // GET /Characters/Detail/5
        public async Task<IActionResult> Detail(int id)
        {
            var character = await _service.GetCharacterByIdAsync(id);
            return View(character);
        }
    }
}