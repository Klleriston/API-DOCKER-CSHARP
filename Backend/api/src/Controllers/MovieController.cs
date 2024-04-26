using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Models;

namespace src.Controllers
{
    [ApiController]
    [Route("/")]
    public class MovieController : Controller
    {
       private readonly IMovieRepository _movieRepository;
       private readonly MovieNetContext _movieNetContext;

        public MovieController(IMovieRepository movieRepository, MovieNetContext movieNetContext)
        {
            _movieRepository = movieRepository;
            _movieNetContext = movieNetContext;
        }

        [HttpPost("/movie")]
        public async Task<IActionResult> createMovie([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest("corpo ta invalido tlg");
            }

            try
            {
                _movieRepository.Create(movie);
                await _movieNetContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, new { success = true, data = movie });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "ops falha aqui :p");
            }
        }

        [HttpGet("/movie/{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie =  _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet("/movie")]
        public async Task<IActionResult> GetMovie()
        {
            var movies = await _movieNetContext.Movies.ToListAsync();
            return Ok(movies);
        }




    }
}
