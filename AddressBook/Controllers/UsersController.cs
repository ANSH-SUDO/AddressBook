using AddressBook.Helpers;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;

namespace AddressBook.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly AddressContext _context;

        public UsersController(JwtService jwtService, AddressContext context)
        {
            _jwtService = jwtService;
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(AddressBookEntryDTO request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
                return BadRequest("Email already exists!");

            var passwordHash = PasswordHasher.HashPassword(request.Password);

            var user = new AddressEntity
            {
                Username = request.Name,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = ""
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully!" });
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(AddressBookEntryDTO request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}
