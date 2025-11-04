using System.ComponentModel.DataAnnotations;

namespace MovieDemo.Api.Models.DTO
{
    public record CreateMovieRequest([Required] string Title, [Range(1900,3000)] int year, [Range(0,10)]double rating);
    public record ChangeRatingRequest([Range(0,10)] double rating);
    public record MovieResponse(int Id, string Title, int Year, double Rating, bool IsAvaliable);
}
