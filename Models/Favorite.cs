using System.ComponentModel.DataAnnotations;

namespace DragonBallMVC.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        [Required]
        public int CharacterId { get; set; }

        [Required]
        public string CharacterName { get; set; } = string.Empty;

        public string CharacterImage { get; set; } = string.Empty;
        public string CharacterRace { get; set; } = string.Empty;

        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}