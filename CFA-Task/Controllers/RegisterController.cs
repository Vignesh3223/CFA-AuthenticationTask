using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserData.IRepository;
using UserData.Models;

namespace CFA_Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public RegisterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<Success> RegisterUser([FromBody] User user)
        {
            var response = await _userRepository.CreateUser(user);
            return response;
        }
    }
}
