using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Smart_Dilation_Management.Data;
using Smart_Dilation_Management.DTO.LoginDTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Dilation_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DilationData _Db;
        private readonly IConfiguration _config;

        public AuthController(DilationData db, IConfiguration config)
        {
            _Db = db;
            _config = config;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            var Data = await _Db.User.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (Data == null)
                return Unauthorized("Invalid credentials");
            bool isValidPassword =
     BCrypt.Net.BCrypt.Verify(request.Password, Data.PasswordHash);
            if (!isValidPassword)
                return Unauthorized("Invalid credentials");
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Data.Id.ToString()),
                new Claim(ClaimTypes.Email, Data.Email),
                new Claim(ClaimTypes.Role, Data.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
