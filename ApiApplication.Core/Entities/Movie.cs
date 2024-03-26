namespace ApiApplication.Core.Entities
{
    public class Movie
    {
        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public string ImdbId { get; protected set; }
        public string Stars { get; protected set; }
        public DateTime ReleaseAtUtc { get; protected set; }

        public int LengthInMinutes { get; protected set; }
        protected Movie()
        {
            
        }
        
        private Movie(string title, string imdbId, string stars, DateTime releaseAtUtc, int lengthInMinutes)
        {
            Title = title;
            ImdbId = imdbId;
            Stars = stars;
            ReleaseAtUtc = releaseAtUtc;
            LengthInMinutes = lengthInMinutes;
        }

        public static Movie Create(string title, string imdbId, string stars, DateTime releaseDate, int lengthInMinutes) =>
            new(title, imdbId, stars, releaseDate, lengthInMinutes);
    }
}
