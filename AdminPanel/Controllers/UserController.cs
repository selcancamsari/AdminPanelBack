using AdminPanel.DTO;
using AdminPanel.Models;
using AdminPanel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            ResponseViewModel result = new ResponseViewModel();
            //Asıl iş business içinde yapılır. Şimdilik böyle yazdım.
            using (var context = new MyDbContext())
            {
                var db = context.Users.Where(s => s.UserName == request.UserName || s.Email == request.Email);

                if (db.Count() > 0)
                {
                    //Bu kullanıcı adı ya da email daha önce alınmış
                    result.isSuccess = false;
                    result.Messages = "User with this User Name or Email already registered";
                    return BadRequest(result);
                }


                UserModel model = new UserModel();

                model.UserName = request.UserName;
                model.Password = request.Password;
                model.Email = request.Email;

                context.Add(model);
                context.SaveChanges();

                result.isSuccess = true;
                return Ok(result);

            }


        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            ResponseViewModel result = new ResponseViewModel();

            using (var context = new MyDbContext())
            {
                UserModel db = context.Users.Where(s => s.UserName == request.UserName && s.Password == request.Password).FirstOrDefault();
                if(db == null)
                {
                    result.isSuccess = false;
                    result.Messages = "User Name or Password is incorrect";
                    return NotFound(result);
                }

                //eğer kullanıcı adı ve parola kısmı doğru ise token verip içeri alacağız

                var token = GenerateToken(db);

                result.isSuccess = true;
                result.Token = token;
                return Ok(result);

            }
        }

        [NonAction]
        public string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
               // new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
