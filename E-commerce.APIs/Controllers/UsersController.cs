using E_commerce.BL.Dtos.Users;
using E_commerce.DAL.Data.Clients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commerce.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        #region  Helpers
        private ActionResult<TokenDto> GenerateToken(IEnumerable<Claim> userClaims)
        {


            var keyFromConfig = _configuration.GetValue<String>(Constants.AppSettings.SecretKey);
            var keyInBytes = Encoding.ASCII.GetBytes(keyFromConfig); //convert string to bytes
            var key = new SymmetricSecurityKey(keyInBytes); //convert arr of bytes to obj


            var sigingCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            // SigningCredentials Want object key not string
            var expiryDateTime = DateTime.Now.AddMinutes(10); //jwt expire date 
            var jwt = new JwtSecurityToken(claims: userClaims, expires: expiryDateTime, signingCredentials: sigingCredential);
            var jwtAsString = new JwtSecurityTokenHandler().WriteToken(jwt);
            //to extract string from jwt

            return new TokenDto(jwtAsString, expiryDateTime);
        }

        #endregion


        #region Register

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                Role = registerDto.Role
            };
            //CreateAsync can create Hashed password
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // mandatory
                 new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim (ClaimTypes.Role, user.Role)
             };


            foreach (var claim in claims)
            {
                await _userManager.AddClaimAsync(user, claim);
            }

            var response = new
            {
                message = "User registered successfully"
            };

            // Returning a 201 Created response
            return StatusCode(StatusCodes.Status201Created, new { Message = "User Registered successfully" });

        }

        #endregion

        #region  Login
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenDto>> Login(LoginDto loginDto)
        {
            //check if  user name exist 
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Incorrect Email or Password");
            }

            //checked if user locked 
            if(await _userManager.IsLockedOutAsync(user))
            {
                return BadRequest("Try Again");
            }
            //check  if password correct
            bool isAuthenticated = await _userManager.CheckPasswordAsync(user, loginDto.Password);//compare two password
            if (!isAuthenticated)
            {
                await _userManager.AccessFailedAsync(user);
                return Unauthorized("Incorrect Email or Password");

            }
            // name and password correct then get cliams from db 

            var userClaims = await _userManager.GetClaimsAsync(user);
            //2- generate token
            return GenerateToken(userClaims);

        }

        #endregion


    }
}
