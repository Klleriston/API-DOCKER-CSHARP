using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.DTO;
using src.Models;
using src.Services;

namespace api.Controllers
{
    [ApiController]
    [Route("/")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly JWTservice _jwtService;
        private readonly RateService _rateService;

        public UserController(IUserRepository userRepository, JWTservice jwtServices, RateService rateService)
        {
            _userRepository = userRepository;
            _jwtService = jwtServices;
            _rateService = rateService;
        }


        [HttpGet("user")]
        public IActionResult GetLoggedInUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                if (string.IsNullOrEmpty(jwt))
                {
                    return Unauthorized(new { message = "JWT token not provided." });
                }

                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);

                var user = _userRepository.GetUserById(userId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized(new { message = "Authorization failed!" });
            }
        }


        [HttpGet("users")]
        public IActionResult GetRegisteredUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("user/movies")]
        public IActionResult GetMoviesEvaluatedByUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);
                int userId = int.Parse(token.Issuer);

                var userMovies = _rateService.GetRatingsForUser(userId);
                return Ok(userMovies);
            }
            catch (Exception)
            {
                return Unauthorized(new { message = "Authorization failed!" });
            }
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDTO registrationDTO)
        {
            try
            {
                if (_userRepository.GetUserByEmail(registrationDTO.Email) != null)
                {
                    return Conflict(new { message = "The user is already registered." });
                }

                var newUser = new User
                {
                    Email = registrationDTO.Email,
                    Password = registrationDTO.Password,
                };
                _userRepository.Create(newUser);

                return Ok(new { message = "User registered successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"ops {ex.Message}" });
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDTO loginDTO)
        {
            try
            {
                var user = _userRepository.GetUserByEmail(loginDTO.Email);

                if (user == null || user.Password != loginDTO.Password)
                {
                    return Unauthorized(new { message = "Invalid Credentials." });
                }

                var token = _jwtService.Generate(user.Id);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Secure = true
                };

                Response.Cookies.Append("jwt", token, cookieOptions);

                return Ok(new { user, message = "sucess!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"ops {ex.Message}" });
            }
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Logged out successfully!" });
        }

        [HttpDelete("user/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);

                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }
                _userRepository.DeleteUser(id);

                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"ops {ex.Message}" });
            }
        }

    }
}