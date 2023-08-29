using AsisProject.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AsisProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Name = model.Name,
                Surname = model.Surname,
                Age = model.Age,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Register-Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            //mevcut admin sayisini almak
            var adminRole = await _roleManager.FindByNameAsync(UserRoles.Admin);
            var adminsCount = adminRole != null ? _userManager.GetUsersInRoleAsync(UserRoles.Admin).Result.Count : 0;

            // Eğer zaten admin varsa ve şu anki kullanıcı admin değilse, yeni admin oluşturmaya izin verme
            if (adminsCount > 0 && !User.IsInRole(UserRoles.Admin))
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    { Status = "Error", Message = "Only an admin can create another admin!." });
            var userExist = await _userManager.FindByNameAsync(model.Username);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Name = model.Name,
                Surname = model.Surname,
                Age = model.Age
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            return Ok(new Response { Status = "Success", Message = "Admin created successfully!" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });

            }

            return Unauthorized(new Response { Status = "Error", Message = "Username or password is incorrect!" });
        }

        [HttpDelete("Delete-User")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound(new Response { Status = "Error", Message = "User not found!" });

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new Response { Status = "Success", Message = "User deleted successfully!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User deletion failed! Please check user details and try again." });

            }
        }

        [HttpPost]
        [Route("Assign-Admin")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> MakeAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "User not found!" });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            return Ok(new Response { Status = "Success", Message = "User is admin now!" });

        }

        [HttpPost]
        [Route("Revoke-Admin")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RevokeAdmin(string id)
        {
            //kullanicinin admin olup olmadigini kontrol et
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new Response { Status = "Error", Message = "User not found!" });
            }
            //kendi yetkilerini kaldirmasini engelle
            if (user.Id == User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return BadRequest(new Response { Status = "Error", Message = "You cannot revoke your own admin rights!" });
            }


            var isAdmin = await _userManager.IsInRoleAsync(user, UserRoles.Admin);
            if (!isAdmin)
            {
                return BadRequest(new Response { Status = "Error", Message = "This user is not an admin!" });
            }

            var result = await _userManager.RemoveFromRoleAsync(user, UserRoles.Admin);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User admin role revocation failed! Please check user details and try again." });
            }

            // Eğer yetkisini çektiysek, "User" rolünü ekleyelim
            result = await _userManager.AddToRoleAsync(user, UserRoles.User);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Failed to update user role to User! Please check user details and try again." });
            }

            return Ok(new Response { Status = "Success", Message = "User's admin rights have been revoked and role updated to User." });
        }

        [HttpGet]
        [Route("Get-Users")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetUser()
        {
            return Ok(_userManager.Users.ToList());
        }

        [HttpGet]
        [Route("Get-Admins")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> ListAdmins()
        {
            return Ok(await _userManager.GetUsersInRoleAsync(UserRoles.Admin));
        }
    }

}

