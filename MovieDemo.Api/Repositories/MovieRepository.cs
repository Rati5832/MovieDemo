using Microsoft.EntityFrameworkCore;
using MovieDemo.Api.Data;
using MovieDemo.Api.Models;

namespace MovieDemo.Api.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _dbContext;

        public MovieRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie> AddAsync(Movie movie)
        {
            await _dbContext.Movies.AddAsync(movie);
            await _dbContext.SaveChangesAsync();
            return movie;
        }

        public Task<bool> ExistsAsync(string title, int year) =>
        _dbContext.Movies.AnyAsync(m => m.Title == title && m.Year == year);

        public Task<Movie?> GetByIdAsync(int id) =>
        _dbContext.Movies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsAvailable);

        public Task<List<Movie>> SearchAsync(int? year, double? minRating)
        {
            var q = _dbContext.Movies.AsNoTracking().Where(x => x.IsAvailable);
            if(year.HasValue)
                q = q.Where(x => x.Year == year);

            if(minRating.HasValue)
                q = q.Where(x => x.Rating == minRating);

            return q.OrderByDescending(m => m.Rating).ThenBy(y => y.Year).ToListAsync();
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var m = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (m == null)
                return false;

            m.IsAvailable = false;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRatingAsync(int id, double newRating)
        {
            var m = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id && x.IsAvailable == true);
            if (m == null) return false;
            m.Rating = newRating;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
