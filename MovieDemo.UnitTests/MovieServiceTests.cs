using FluentAssertions;
using Moq;
using MovieDemo.Api.Repository;
using MovieDemo.Api.Services;

namespace MovieDemo.UnitTests
{
    public class MovieServiceTests
    {
        [Fact]
        public async Task CreateAsync_Should_Fail_When_TitleMissing()
        {
            var repo = new Mock<IMovieRepository>();
            var svc = new MovieService(repo.Object);

            var (ok, error, movie) = await svc.CreateAsync("", 2020, 8.5);

            ok.Should().BeFalse();
            error.Should().Be("Title is required.");
            movie.Should().BeNull();
        }


        [Fact]
        public async Task CreateAsync_Should_Fail_When_Rating_OutOfRange()
        {
            var svc = new MovieService(new Mock<IMovieRepository>().Object);

            var (ok, error, _) = await svc.CreateAsync("Jesse James", 2010, 100);
            ok.Should().BeFalse();
            error.Should().Be("Rating must be between 0 and 10");
        }

        [Fact]
        public async Task ChangeRating_Should_Block_Invalid_Range()
        {
            var svc = new MovieService(new Mock<IMovieRepository>().Object);
            var (ok, error) = await svc.ChangeRatingAsync(1, -1);

            ok.Should().BeFalse();
            error.Should().Be("New Rating should be between 0 and 10");
        }

        [Fact]
        public async Task ChangeRating_Should_Succeed_And_Call_Repo()
        {
            var repo = new Mock<IMovieRepository>();
            repo.Setup(r => r.UpdateRatingAsync(1, 8.8)).ReturnsAsync(true);

            var svc = new MovieService(repo.Object);

            var (ok, error) = await svc.ChangeRatingAsync(1, 8.8);

            ok.Should().BeTrue();
            error.Should().BeNull();

            repo.Verify(r => r.UpdateRatingAsync(1, 8.8), Times.Once);
        }


        [Fact]
        public async Task Delete_Should_Be_False_Calling_Repo()
        {
            var repo = new Mock<IMovieRepository>();
            repo.Setup(x => x.SoftDeleteAsync(0)).ReturnsAsync(false);

            var service = new MovieService(repo.Object);
            var failDelete = await service.DeleteAsync(0);

            failDelete.Should().BeFalse();
            repo.Verify(x => x.SoftDeleteAsync(0), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_Be_Succeed_And_Call_Repo()
        {
            var repo = new Mock<IMovieRepository>();
            repo.Setup(x => x.SoftDeleteAsync(1)).ReturnsAsync(true);

            var svc = new MovieService(repo.Object);
            var successDelete = await svc.DeleteAsync(1);

            successDelete.Should().BeTrue();
            repo.Verify(x => x.SoftDeleteAsync(1), Times.Once);
        }
    }
}