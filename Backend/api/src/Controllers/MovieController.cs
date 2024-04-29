using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MovieNetContext _movieNetContext;
        private readonly MovieService _movieService;

        public MovieController(MovieNetContext movieNetContext, MovieService movieService)
        {
            _movieNetContext = movieNetContext;
            _movieService = movieService;
        }

        [HttpPost("create")]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest("Invalid request body.");
            }

            _movieService.Create(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        [HttpGet("movie/{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound("Movie not found.");
            }
            return Ok(movie);
        }

        [HttpGet("movie")]
        public IActionResult GetMovies()
        {
            IEnumerable<Movie> movies = _movieService.GetAllMovies();
            if (movies == null || !movies.Any())
            {
                return NoContent();
            }
            return Ok(movies);
        }

        [HttpPut("movie/update/{id}")]
        public ActionResult UpdateMovie(int id, [FromBody] Movie movie)
        {
            if (movie == null || id != movie.Id)
            {
                return BadRequest("Invalid request body or ID.");
            }

            _movieService.UpdateMovie(movie);
            return Ok("Movie updated successfully.");
        }

        [HttpDelete("movie/delete/{id}")]
        public ActionResult DeleteMovie(int id)
        {
            _movieService.DeleteMovie(id);
            return Ok("Movie deleted successfully.");
        }
    }
}
