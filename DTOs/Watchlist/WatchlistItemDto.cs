namespace StreamListAPI.DTOs.Watchlist
{
    public class WatchlistItemDto
    {
        public string TmdbId { get; set; } = "";
        public string MediaType { get; set; } = "";   // "movie" ou "tv"
        public string Title { get; set; } = "";
        public string PosterPath { get; set; } = "";
        public string Status { get; set; } = "want";  // "want", "watching", "done"
    }
}
