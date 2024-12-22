using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;
using System.Security.Claims;
using System.Text;
using Entities;
using SEP3_T3_ASP_Core_WebAPI.ApiContracts.AuthDtos;
using Microsoft.AspNetCore.Authorization;

namespace SEP3_T3_ASP_Core_WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        public AuthController(IAuthRepository authRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            this.authRepository = authRepository;
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid login request");
            }

            var user = await authRepository.LoginAsync(loginRequest.UserName, loginRequest.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var token = GenerateJwtToken(user);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes("xgjpxug32J3rW0pICEadjgUIPj/TrwGl57wNvwJQJms=");

            var claims = new List<Claim>
            {
                new Claim("sub", user.UserId.ToString()),
                new Claim("unique_name", user.UserName),
                new Claim(ClaimTypes.Role, user.UserRole.ToString()),
                new Claim("jti", Guid.NewGuid().ToString())
            };

            // Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            // Use custom TokenHandler
            var tokenHandler = new TokenHandler();
            return tokenHandler.CreateToken(tokenDescriptor);
        }
    }

    public class TokenHandler
    {
        public string CreateToken(SecurityTokenDescriptor tokenDescriptor)
        {
            var key = tokenDescriptor.SigningCredentials.Key as SymmetricSecurityKey;
            var signingAlgorithm = tokenDescriptor.SigningCredentials.Algorithm;

            // Encode header
            var header = new
            {
                alg = signingAlgorithm,
                typ = "JWT"
            };
            var headerJson = System.Text.Json.JsonSerializer.Serialize(header);
            var headerBase64 = Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(headerJson));

            // Encode payload
            var payload = new Dictionary<string, object>
            {
                { "exp", new DateTimeOffset(tokenDescriptor.Expires ?? DateTime.UtcNow.AddHours(1)).ToUnixTimeSeconds() }
            };
            if (tokenDescriptor.Subject != null)
            {
                foreach (var claim in tokenDescriptor.Subject.Claims)
                {
                    payload[claim.Type] = claim.Value;
                }
            }
            var payloadJson = System.Text.Json.JsonSerializer.Serialize(payload);
            var payloadBase64 = Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(payloadJson));

            // Create signature
            var headerPayload = $"{headerBase64}.{payloadBase64}";
            var signature = CreateSignature(headerPayload, key, signingAlgorithm);
            var signatureBase64 = Base64UrlEncoder.Encode(signature);

            // Return the complete JWT
            return $"{headerPayload}.{signatureBase64}";
        }

        public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters)
        {
            var parts = token.Split('.');
            if (parts.Length != 3)
            {
                throw new SecurityTokenInvalidSignatureException("JWT must have 3 parts");
            }

            var header = Base64UrlEncoder.Decode(parts[0]);
            var payload = Base64UrlEncoder.Decode(parts[1]);
            var signature = Base64UrlEncoder.DecodeBytes(parts[2]);

            var headerPayload = $"{parts[0]}.{parts[1]}";
            var key = validationParameters.IssuerSigningKey as SymmetricSecurityKey;
            var signingAlgorithm = validationParameters.ValidAlgorithms.FirstOrDefault();

            var expectedSignature = CreateSignature(headerPayload, key, signingAlgorithm);
            if (!signature.SequenceEqual(expectedSignature))
            {
                throw new SecurityTokenInvalidSignatureException("Signature validation failed");
            }

            var claims = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(payload);
            var identity = new ClaimsIdentity(claims.Select(kv => new Claim(kv.Key, kv.Value.ToString() ?? "")));
            return new ClaimsPrincipal(identity);
        }

        private byte[] CreateSignature(string input, SymmetricSecurityKey key, string algorithm)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA256(key.Key);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
        }
    }
}
