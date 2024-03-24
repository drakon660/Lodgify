namespace ApiApplication.Core.Entities
{
    public class Movie
    {
        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public string ImdbId { get; protected set; }
        public string Stars { get; protected set; }
        public DateTime ReleaseDate { get; protected set; }
        public List<Showtime> Showtimes { get; set; }
        
        private Movie(string title, string imdbId, string stars, DateTime releaseDate)
        {
            Title = title;
            ImdbId = imdbId;
            Stars = stars;
            ReleaseDate = releaseDate;
        }

        public static Movie Create(string title, string imdbId, string stars, DateTime releaseDate) =>
            new(title, imdbId, stars, releaseDate);
    }
}
