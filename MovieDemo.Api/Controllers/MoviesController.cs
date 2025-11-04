using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieDemo.Api.Models.DTO;
using MovieDemo.Api.Services;

namespace MovieDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieResponse>> Get(int id)
        {
            var movie = await _movieService.GetAsync(id);
            if (movie == null) return NotFound();

            return Ok(new MovieResponse(movie.Id, movie.Title, movie.Year, movie.Rating, movie.IsAvailable));
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieResponse>>> Search([FromQuery] int? year, [FromQuery] double? minRating)
        {
            var movieList = await _movieService.SearchAsync(year,minRating);

            return Ok(movieList.Select(m => new MovieResponse(m.Id, m.Title, m.Year, m.Rating, m.IsAvailable)).ToList());
        }

        [HttpPost]
        public async Task<ActionResult<MovieResponse>> Create([FromBody] CreateMovieRequest movieRequest)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var (ok, error, movie) = await _movieService.CreateAsync(movieRequest.Title, movieRequest.year, movieRequest.rating);
            if (!ok) return BadRequest(new { error });
            var response = new MovieResponse(movie!.Id, movie.Title, movie.Year, movie.Rating, movie.IsAvailable);

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, response);
        }

        [HttpPatch("{id:int}/rating")]
        public async Task <IActionResult> ChangeRating(int id, [FromBody] ChangeRatingRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var (ok,error) = await _movieService.ChangeRatingAsync(id, request.rating);
            return ok ? NoContent() : BadRequest(new { error });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _movieService.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
 