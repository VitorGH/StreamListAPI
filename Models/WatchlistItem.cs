namespace StreamListAPI.Models
{
    public class WatchlistItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public string TmdbId { get; set; } = "";       // ID do TMDB
        public string MediaType { get; set; } = "";    // "movie" ou "tv"
        public string Title { get; set; } = "";
        public string PosterPath { get; set; } = "";

        public string Status { get; set; } = "want";   // "want", "watching", "done"
        public int? Rating { get; set; }               // 1 a 5
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
