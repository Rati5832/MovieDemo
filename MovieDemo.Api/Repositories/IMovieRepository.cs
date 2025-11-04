using MovieDemo.Api.Models;

namespace MovieDemo.Api.Repository
{
    public interface IMovieRepository
    {
        Task<Movie?> GetByIdAsync(int id);
        Task<List<Movie>> SearchAsync(int? year, double? minRating);
        Task<bool> ExistsAsync(string title, int year);
        Task<Movie> AddAsync(Movie movie);
        Task<bool> UpdateRatingAsync(int id, double newRating);
        Task<bool> SoftDeleteAsync(int id);
    }
}
