using Microsoft.AspNetCore.Mvc;
using MZWalksApi_6.Repositories;

namespace MZWalksApi_6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }



        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {

            //check if user is authenticated
            //check username and password
            var user = await userRepository.AuthenticateAsync(loginRequest.Username, loginRequest.Password);

            if (user != null)
            {
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Username or Password is InCorrect");

        }
    }
}
