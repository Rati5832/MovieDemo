using MovieDemo.Api.Models;
using MovieDemo.Api.Repository;

namespace MovieDemo.Api.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepo;

        public MovieService(IMovieRepository movieRepo)
        {
            _movieRepo = movieRepo;
        }

        public async Task<(bool ok, string? error)> ChangeRatingAsync(int id, double newRating)
        {
            if (newRating is < 0 or > 10) return (false, "New Rating should be between 0 and 10");
            var ok = await _movieRepo.UpdateRatingAsync(id, newRating);

            return ok ? (true, null) : (false, "Movie not found or unavailable");
        }

        public async Task<(bool ok, string? error, Movie? movie)> CreateAsync(string title, int year, double rating)
        {
            if(string.IsNullOrWhiteSpace(title)) return (false, "Title is required.", null);
            if(year < 1900) return (false, "Year must be >= 1900", null);
            if (rating is < 0 or > 10) return (false, "Rating must be between 0 and 10", null);

            if (await _movieRepo.ExistsAsync(title, year)) return (false,"Movie already exists for this year", null);

            var movie = await _movieRepo.AddAsync(new Movie { Title = title.Trim(), Year = year, Rating = rating });
            return (true, null, movie);

        }

        public Task<bool> DeleteAsync(int id) => _movieRepo.SoftDeleteAsync(id);

        public Task<Movie?> GetAsync(int id) => _movieRepo.GetByIdAsync(id);

        public Task<List<Movie>> SearchAsync(int? year, double? minRating) => _movieRepo.SearchAsync(year, minRating);
    }
}
