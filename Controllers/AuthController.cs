using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseTracker.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwt;

    public AuthController(AppDbContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            return BadRequest("Username already exists");

        using var sha = SHA256.Create();
        string hashed = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = hashed
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User created");
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        using var sha = SHA256.Create();
        string hashed = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));

        var existing = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username && u.PasswordHash == hashed);

        if (existing == null)
            return Unauthorized("Invalid credentials");
        string token = _jwt.GenerateToken(dto.Username);
        return Ok(new { token });
    }
}