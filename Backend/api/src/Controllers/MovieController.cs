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
        private readonly MovieNetContext _movieNetContext;

        public MovieController(MovieNetContext movieNetContext)
        {
            _movieNetContext = movieNetContext;
        }

        [HttpPost("/movie/create")]
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

                return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, new { success = true, data = movie });
            }
            catch (Exception)
            {
                return StatusCode(500, "ops falha aqui :p");
            }
        }

        [HttpGet("/movie/{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _movieRepository.GetMovieById(id);
            return Ok(movie);
        }

        [HttpGet("/movie")]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieNetContext.Movies.ToListAsync();
            return Ok(movies);
        }

        [HttpPut("/movie/update/{id}")]
        public async Task<IActionResult> UpdatedMovie([FromBody] Movie movie, int id)
        {
            if (movie == null) 
            {
                return BadRequest("Mandou errado de novo mano ?");
            }

            try
            {
                var updatedMovie = _movieRepository.GetMovieById(id);

                updatedMovie.Title = movie.Title;
                updatedMovie.Description = movie.Description;
                updatedMovie.Year = movie.Year;

                _movieNetContext.Update(updatedMovie);
                await _movieNetContext.SaveChangesAsync();

                return Ok("Filme atualizado !!");
            }
            catch (Exception)
            {
                return BadRequest("Deu ruim no update xD");
            }
        }

    
    }
}
