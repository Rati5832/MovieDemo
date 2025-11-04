using FluentAssertions;
using MovieDemo.Api.Models.DTO;
using System.Net;
using System.Net.Http.Json;

namespace MovieDemo.IntegrationTests
{
    public class MoviesApiTests : IClassFixture<CustomWebAppFactory>
    {
        private readonly HttpClient _client;

        public MoviesApiTests(CustomWebAppFactory f)
        {
            _client = f.CreateClient();
        }

        [Fact]
        public async Task Create_Then_Get_Should_Work()
        {
            // create
            var create = new CreateMovieRequest("Inception", 2010, 9.0);
            var post = await _client.PostAsJsonAsync("/api/movies", create);

            post.StatusCode.Should().Be(HttpStatusCode.Created);

            var created = await post.Content.ReadFromJsonAsync<MovieResponse>();
            created!.Title.Should().Be("Inception");

            // by id
            var get = await _client.GetAsync($"/api/movies/{created.Id}");
            get.StatusCode.Should().Be(HttpStatusCode.OK);

            var fetched = await get.Content.ReadFromJsonAsync<MovieResponse>();
            fetched.Rating.Should().Be(9.0);
        }

        [Fact]
        public async Task Patch_Rating_Should_Return_NoContent_On_Success()
        {
            var post = await _client.PostAsJsonAsync("/api/movies", new CreateMovieRequest("The Game", 2000, 8.0));
            var movie = await post.Content.ReadFromJsonAsync<MovieResponse>();

            var patch = await _client.PatchAsJsonAsync($"/api/movies/{movie.Id}/rating", new ChangeRatingRequest(10.0));
            patch.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var get = await _client.GetAsync($"/api/movies/{movie.Id}");
            var fetched = await get.Content.ReadFromJsonAsync<MovieResponse>();
            fetched!.Rating.Should().Be(10.0);
        }

        [Fact]
        public async Task Delete_Should_SoftDelete()
        {
            var post = await _client.PostAsJsonAsync("/api/movies", new CreateMovieRequest("Interstellar", 2014, 9.1));
            var movie = await post.Content.ReadFromJsonAsync<MovieResponse>();

            var delete = await _client.DeleteAsync($"/api/movies/{movie.Id}");
            delete.StatusCode.Should().Be(HttpStatusCode.NoContent);

            var get = await _client.GetAsync($"/api/movies/{movie.Id}");
            get.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

    }
}
