using HRIS.data;
using HRIS.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HRIS.Controller
{
    [ApiController]
    [Route("api")]
    public class AuthController : ControllerBase
    {

        private readonly DataContextEF _db;

        private readonly IConfiguration _config;

        public AuthController(DataContextEF db, IConfiguration config)
        {
            _db = db;
            _config = config;
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


    }
}