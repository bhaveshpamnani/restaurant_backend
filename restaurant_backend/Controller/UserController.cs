using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using restaurant_backend.Data;
using restaurant_backend.Model;

namespace restaurant_backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository UserRepository)
        {
            _userRepository = UserRepository;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            var User = _userRepository.GetAllUser();
            return Ok(User);
        }

        [HttpGet("{UserID}")]
        public IActionResult GetUserProfile(int UserID)
        {
            var User = _userRepository.GetUserProfile(UserID);
            return Ok(User);
        }

        [HttpPost("signup")]
        public IActionResult SignUpUser(UserModel UserModel)
        {
            var value = _userRepository.SignUpUser(UserModel);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateUser(UserModel UserModel)
        {
            var value = _userRepository.UpdateUser(UserModel);
            return Ok(value);
        }
        
        [HttpDelete("{UserID}")]
        public IActionResult DeleteUser(int UserID)
        {
            var value = _userRepository.DeleteUser(UserID);
            return Ok(value);
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var response = _userRepository.Login(login.Email, login.Password);
            if (response != null)
            {
                return Ok(response); // Returns token and userId as JSON
            }
            return Unauthorized("Invalid credentials");
        }
    }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
