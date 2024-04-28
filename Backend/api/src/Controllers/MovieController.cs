using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly MovieRepository _movieRepository;

        public MovieController(MovieNetContext movieNetContext, MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            _movieNetContext = movieNetContext;
        }

        [HttpPost("/movie/create")]
        public IActionResult createMovie([FromBody] Movie movie)
        {
            if (movie == null)
            {
                return BadRequest("corpo ta invalido tlg");
            }

            _movieRepository.Create(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        [HttpGet("/movie/{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = _movieRepository.GetMovieById(id);
            if (movie == null) 
            {
                return BadRequest("ih deu ruim ai kkkk");
            }
            return Ok(movie);
        }

        [HttpGet("/movie")]
        public IActionResult GetMovies()
        {
            IEnumerable<Movie> movies = _movieRepository.GetAllMovies();
            if (movies == null) 
            {
                return NoContent();
            }
            return Ok(movies);
        }

        [HttpPut("/movie/update/{id}")]
        public ActionResult UpdatedMovie([FromBody] Movie movie, int id)
        {
            if (movie == null || id == movie.Id) 
            {
                return BadRequest("Mandou errado de novo mano ?");
            }

            _movieNetContext.Update(movie);
            return Ok("Atualização feita com sucesso capitao broxa");
        }

        [HttpDelete("/movie/delete/{id}")]
        public ActionResult DeleteMovie(int id)
        {
            _movieRepository.DeleteMovie(id);
            return Ok("Excluido !!");
        }
    
    }
}
