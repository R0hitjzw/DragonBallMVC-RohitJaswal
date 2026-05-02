using Newtonsoft.Json;

namespace DragonBallMVC.Models
{
    public class DragonBallApiResponse
    {
        [JsonProperty("items")]
        public List<Character> Items { get; set; } = new();

        [JsonProperty("meta")]
        public ApiMeta Meta { get; set; } = new();
    }

    public class ApiMeta
    {
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }

        [JsonProperty("itemCount")]
        public int ItemCount { get; set; }

        [JsonProperty("itemsPerPage")]
        public int ItemsPerPage { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }
    }
}