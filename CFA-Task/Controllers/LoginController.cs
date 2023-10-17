using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserData.Models;

namespace CFA_Task.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserContext _context;
        public LoginController(UserContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult UserLogin([FromBody] Login login)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == login.Email && x.Password == login.Password);
            if (user == null)
            {
                return BadRequest(new UserData.Models.Response
                {
                    Status = "Failed",
                    Message = "User not found",
                    Token = "Invalid"
                });
            }
            else
            {
                IActionResult token = GetToken(user);
                if (token is OkObjectResult objectResult)
                {
                    string? jwtToken = objectResult.Value?.ToString();
                    var response = new UserData.Models.Response
                    {
                        Status = "Success",
                        Message = "Login Success",
                        Token = jwtToken
                    };
                    return Ok(response);
                }
                else
                {
                    return BadRequest(new UserData.Models.Response
                    {
                        Status = "Failed",
                        Message = "Token Generation Failed",
                        Token = "Null"
                    });
                }
            }
        }

        [HttpPost]
        public IActionResult GetToken(User user)
        {
            var key = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs05Gty";
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Firstname),
                new Claim(ClaimTypes.Name, user.Lastname),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: "JWTAuthenticationServer",
                audience: "JWTServicePostmanClient",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwtToken);
        }
    }
}
