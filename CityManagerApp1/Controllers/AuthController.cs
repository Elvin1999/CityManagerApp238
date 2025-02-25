using CityManagerApp1.Dtos;
using CityManagerApp1.Entities;
using CityManagerApp1.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityManagerApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthRepository _authRepository;
        private IConfiguration _configuration;

        public AuthController(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]UserForRegisterDto dto)
        {
            if(await _authRepository.UserExists(dto.Username))
            {
                ModelState.AddModelError("Username", "Username already exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                Username = dto.Username,
            };
            await _authRepository.Register(userToCreate,dto.Password);
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}
