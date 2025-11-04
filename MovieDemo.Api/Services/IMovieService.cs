using MovieDemo.Api.Models;

namespace MovieDemo.Api.Services
{
    public interface IMovieService
    {
        Task<Movie?> GetAsync(int id);
        Task<List<Movie>> SearchAsync(int? year, double? minRating);
        Task<(bool ok, string? error, Movie? movie)> CreateAsync(string title, int year, double rating);
        Task<(bool ok, string? error)> ChangeRatingAsync(int id, double newRating);
        Task<bool> DeleteAsync(int id);
    }
}
