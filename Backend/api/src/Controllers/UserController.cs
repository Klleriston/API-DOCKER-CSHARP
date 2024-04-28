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

        public UserController(IUserRepository userRepository, JWTservice jwtServices)
        {
            _userRepository = userRepository;
            _jwtService = jwtServices;
        }

        [HttpPost("/register")]
        public IActionResult Register(UserRegisterDTO dTO)
        {
            var user = new User
            {
                Name = dTO.Name,
                Email = dTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dTO.Password)
            };
            return Created("Sucesso !", _userRepository.Create(user));
        }

        [HttpPost("/login")]
        public IActionResult Login(UserLoginDTO loginDTO)
        {
            var user = _userRepository.GetUserByEmail(loginDTO.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(
                loginDTO.Password, user.Password
                ))
                return BadRequest(new
                {
                    message = "Credenciais Invalidas!"
                });

            var jwt = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            return Ok(new
            {
                message = "Sucesso !"
            });
        }

        [HttpGet("user")]
        public IActionResult UserC()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);
                int userID = int.Parse(token.Issuer);

                var user = _userRepository.GetUserById(userID);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized(new
                {
                    message = "Te falta autorização"
                });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "Saiu de boa"
            });
        }
    }
}