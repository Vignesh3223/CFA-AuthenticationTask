using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserData.IRepository;
using UserData.Models;

namespace CFA_Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthUserInfoController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AuthUserInfoController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<User>> GetUsers()
        {
            var response = await _userRepository.GetUsers();
            return response;
        }

        [HttpGet]
        [Authorize]
        public async Task<User> GetUserById(int Userid)
        {
            var response = await _userRepository.GetUserById(Userid);
            return response;
        }
    }
}
