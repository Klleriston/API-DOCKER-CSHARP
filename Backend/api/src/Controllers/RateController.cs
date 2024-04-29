using Microsoft.AspNetCore.Mvc;
using src.Services;

[ApiController]
[Route("api/[controller]")]
public class RateController : ControllerBase
{
    private readonly RateService _ratingService;
    private readonly JWTservice _jWTservice;

    public RateController(RateService ratingService, JWTservice jWTservice)
    {
        _ratingService = ratingService;
        _jWTservice = jWTservice;
    }

    [HttpPost("{movieId}/rate")]
    public IActionResult RateMovie(int movieId, [FromBody] int rating)
    {
        try
        {
            if (rating < 1 || rating > 5)
            {
                return BadRequest("Invalid rating value. Rating must be between 1 and 5.");
            }
            var jwt = Request.Cookies["jwt"];
            var token = _jWTservice.Verify(jwt);
            int userId = int.Parse(token.Issuer);

            _ratingService.AddRating(userId, movieId, rating);
            return Ok("Movie rating added successfully.");
        }
        catch (Exception)
        {
            return Unauthorized(new { message = "Authorization failed!" });
        }
    }

    [HttpGet("ratings")]
    public IActionResult GetMovieRatings()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];
            var token = _jWTservice.Verify(jwt);
            int userId = int.Parse(token.Issuer);

            var movieRatings = _ratingService.GetRatingsForUser(userId);
            return Ok(movieRatings);
        }
        catch (Exception)
        {
            return Unauthorized(new { message = "Authorization failed!" });
        }
    }
}
