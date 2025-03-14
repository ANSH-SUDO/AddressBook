using AddressBook.Helpers;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using RepositoryLayer.Context;
using System;

namespace AddressBook.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AddressContext _context;
        private readonly EmailService _emailService;

        public AuthController(AddressContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordDTO request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return BadRequest("User not found");

            // Generate reset token
            user.ResetToken = TokenGenerator.GenerateToken();
            user.ResetTokenExpiry = DateTime.UtcNow.AddHours(1);
            _context.SaveChanges();

            // Send email with reset link
            var resetLink = $"https://localhost:5001/reset-password?token={user.ResetToken}";
            _emailService.SendEmailAsync(user.Email, "Password Reset",
                $"Click <a href='{resetLink}'>here</a> to reset your password.");

            return Ok(new { message = "Password reset link sent to your email." });
        }

        [HttpPost]
        [Route("reset-password")]
        public IActionResult ResetPassword(ResetPasswordDTO request)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.ResetToken == request.Token &&
                u.ResetTokenExpiry > DateTime.UtcNow);

            if (user == null)
                return BadRequest("Invalid or expired token");

            // Hash new password
            user.PasswordHash = PasswordHasher.HashPassword(request.NewPassword);
            user.ResetToken = null;
            user.ResetTokenExpiry = null;

            _context.SaveChanges();

            return Ok(new { message = "Password reset successfully!" });
        }
    }
}
