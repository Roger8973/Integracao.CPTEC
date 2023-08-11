using Integracao.CPTEC.API.DTOs;
using Integracao.CPTEC.Infra.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;

namespace Integracao.CPTEC.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AccountsController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegister registerUser)
        {
            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost("sign-in")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] UserLogin login)
        {
            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, true);

            if (result.IsLockedOut)
                return BadRequest("Account blocked");

            if (!result.Succeeded)
                return BadRequest("Invalid username or password");

            var at = await TokenService.GenerateAccessToken(_userManager, _jwtService, login.Email);
            var rt = await TokenService.GenerateRefreshToken(_userManager, _jwtService, login.Email);
            return Ok(new UserLoginResponse(at, rt));
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] Token token)
        {
            var handler = new JsonWebTokenHandler();

            var result = handler.ValidateToken(token.RefreshToken, new TokenValidationParameters()
            {
                ValidIssuer = "https://refreshtoken.test",
                ValidAudience = "Integracao.CPTEC.API",
                RequireSignedTokens = false,
                IssuerSigningKey = await _jwtService.GetCurrentSecurityKey(),
            });

            if (!result.IsValid)
                return BadRequest("Expired token");

            var user = await _userManager.FindByEmailAsync(result.Claims[JwtRegisteredClaimNames.Email].ToString());
            var claims = await _userManager.GetClaimsAsync(user);

            if (!claims.Any(c => c.Type == "LastRefreshToken" && c.Value == result.Claims[JwtRegisteredClaimNames.Jti].ToString()))
                return BadRequest("Expired token");

            if (user.LockoutEnabled)
                if (user.LockoutEnd < DateTime.Now)
                    return BadRequest("User blocked");

            if (claims.Any(c => c.Type == "TenhoQueRelogar" && c.Value == "true"))
                return BadRequest("User must login again");


            var at = await TokenService.GenerateAccessToken(_userManager, _jwtService, result.Claims[JwtRegisteredClaimNames.Email].ToString());
            var rt = await TokenService.GenerateRefreshToken(_userManager, _jwtService, result.Claims[JwtRegisteredClaimNames.Email].ToString());

            return Ok(new UserLoginResponse(at, rt));
        }

    }
}