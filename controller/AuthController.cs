using HRIS.data;
using HRIS.models;
using HRIS.JWT;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HRIS.Controller
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {

        private readonly DataContextEF _db;
        private readonly GenerateJwt _helperJwt;
        public AuthController(DataContextEF db, IConfiguration config)
        {
            _db = db;
            _helperJwt = new GenerateJwt(config);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (await _db.Users.AnyAsync(u => u.Email == dto.Email))
            {
                return BadRequest("Email sudah digunakan");
            }

            string hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            UserRole role = dto.Role ?? UserRole.KARYAWAN;

            User user = new User
            {
                Email = dto.Email,
                PasswordHash = hash,
                Role = role.ToString().ToUpper(),
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return Ok("Register Berhasil");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            User? user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized("Email atau password salah");
            }

            string token = _helperJwt.GenerateToken(user);
            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            string? email = User.FindFirstValue(ClaimTypes.Email);
            string? role = User.FindFirstValue(ClaimTypes.Role);
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(new
            {
                Email = email,
                Role = role,
                UserID = userId
            });

        }
    }
}