namespace MovieDemo.Api.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Rating { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}
