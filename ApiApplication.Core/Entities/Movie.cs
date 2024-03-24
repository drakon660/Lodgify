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

        public int LengthInMinutes { get; protected set; }
        protected Movie()
        {
            
        }
        
        private Movie(string title, string imdbId, string stars, DateTime releaseDate, int lengthInMinutes)
        {
            Title = title;
            ImdbId = imdbId;
            Stars = stars;
            ReleaseDate = releaseDate;
            LengthInMinutes = lengthInMinutes;
        }

        public static Movie Create(string title, string imdbId, string stars, DateTime releaseDate, int lengthInMinutes) =>
            new(title, imdbId, stars, releaseDate, lengthInMinutes);
    }
}
