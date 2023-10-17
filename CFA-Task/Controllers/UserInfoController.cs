using CFA_Task.CustomAttribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserData.IRepository;
using UserData.Models;

namespace CFA_Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserInfoController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Restrict]
        public async Task<List<User>> GetUsers()
        {
            var response = await _userRepository.GetUsers();
            return response;
        }

        [HttpGet]
        [Restrict]
        public async Task<User> GetUserById(int Userid)
        {
            var response = await _userRepository.GetUserById(Userid);
            return response;
        }
    }
}
