using Api_FiesteDocs.Data;
using Api_FiesteDocs.Entities;
using Api_FiesteDocs.Models;
using Api_FiesteDocs.Services.Interfaces;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_FiesteDocs.Services
{
    public class S_Autenticar:I_Autenticar
    {
        private readonly ApplicationDbContext _context;
        private readonly string secretKey;

        public S_Autenticar(ApplicationDbContext context, IConfiguration config)
        {
            secretKey = config.GetSection("settings").GetSection("SecretKey").ToString();
            _context = context;
        }

        public string Autenticar(Autenticacion user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Correo) || string.IsNullOrWhiteSpace(user.Contrasena))
                return null;

            var correo = user.Correo.Trim().ToLowerInvariant();

            Usuario usuario = _context.Usuarios
                .AsNoTracking()
                .FirstOrDefault(u => u.Correo.ToLower() == correo);

            if (usuario == null)
                return null;

            bool passwordOk = BCrypt.Net.BCrypt.Verify(user.Contrasena, usuario.Contrasena ?? string.Empty);
            if (!passwordOk)
                return null;

            var KeyBytes = Encoding.UTF8.GetBytes(secretKey);

            var claims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Correo ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

            if (usuario.IdRol.HasValue)
                claims.AddClaim(new Claim(ClaimTypes.Role, usuario.IdRol.Value.ToString()));

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(60), // ajustar según política
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(KeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenConfig = TokenHandler.CreateToken(TokenDescriptor);
            return TokenHandler.WriteToken(TokenConfig);
        }

    }
}
