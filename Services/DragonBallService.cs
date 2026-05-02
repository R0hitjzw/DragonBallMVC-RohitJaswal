using DragonBallMVC.Models;
using Newtonsoft.Json;

namespace DragonBallMVC.Services
{
    public class DragonBallService
    {
        private readonly HttpClient _http;
        private const string BaseUrl = "https://dragonball-api.com/api";

        public DragonBallService(HttpClient http)
        {
            _http = http;
        }

        public async Task<DragonBallApiResponse> GetCharactersAsync(int page = 1, int limit = 12)
        {
            var json = await _http.GetStringAsync($"{BaseUrl}/characters?page={page}&limit={limit}");
            return JsonConvert.DeserializeObject<DragonBallApiResponse>(json) ?? new DragonBallApiResponse();
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            var json = await _http.GetStringAsync($"{BaseUrl}/characters/{id}");
            return JsonConvert.DeserializeObject<Character>(json) ?? new Character();
        }
    }
}