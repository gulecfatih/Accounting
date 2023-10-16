using AccountingWebApi.Data.Auth;
using AccountingWebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using AccountingWebApi.Business;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Data;

namespace AccountingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly AppDbContext _context;
        public readonly IConfiguration _configuration;
        public readonly IDistributedCache _cache;
        public AuthController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext context, IConfiguration configuration, IDistributedCache cache)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _cache = cache;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                Name = registerModel.Name,
                Surname = registerModel.SurName,
            };
            
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            var Roleresult = await _userManager.AddToRoleAsync(user, registerModel.RoleName);

            if (result.Succeeded && Roleresult.Succeeded)
                return Ok(result);
            else
                return BadRequest();
        }

        [HttpPost]  
        [Route("RoleAdd")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Rol adı boş olamaz.");

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
                return BadRequest("Bu rol zaten var.");

            var role = new IdentityRole { Name = roleName };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
                return Ok("Rol başarıyla oluşturuldu.");

            return BadRequest("Rol oluşturma işlemi başarısız.");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            string userRole = "";
            if (user != null)
            {
                var userRoles = await _context.UserRoles.Where(u => u.UserId == user.Id).FirstOrDefaultAsync();
                var role = await _context.Roles.Where(u => u.Id == userRoles.RoleId).FirstOrDefaultAsync();
                if (role != null)
                {
                    userRole = role.Name.ToString();
                }
                else
                    return BadRequest("Rol Bulunamadı.");
                var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
                if (result)
                {
                    JwtLoginLogic jwtLoginLogic = new JwtLoginLogic(_configuration,_cache);
                    string token = jwtLoginLogic.GenerateToken(user.Id, userRole);
                    jwtLoginLogic.ValidationToken(user.Id, token);
                    if(token != "" && token != null)
                        return Ok(token);
                }
                   
                else
                    return StatusCode((int)System.Net.HttpStatusCode.Unauthorized, "Kullanıcı adı veya parola hatalı");
            }
            return StatusCode((int)System.Net.HttpStatusCode.Unauthorized, "Kullanıcı adı veya parola hatalı");
        }

        [HttpGet]
        [Route("TokenWithUserId")]
        public async Task<IActionResult> TokenWithUserId(string token)
        {
            string userId = JwtLoginLogic.TokenWithUserId(token);
            if (userId != "")
                 return Ok(userId);
            else
                return BadRequest();

        }

        [HttpGet]
        [Route("TokenWithRole")]
        public async Task<IActionResult> TokenWithRole(string token)
        {

            string role = JwtLoginLogic.TokenWithRole(token);
            if (role != "")
                return Ok(role);
            else
                return BadRequest();
        }

        


    }
}
