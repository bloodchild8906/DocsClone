using DocsClone.Domain.Entities;
using DocsClone.Domain.Interfaces;
using DocsClone.Dto.V1.Account.Request;
using DocsClone.Dto.V1.Account.Response;
using DocsClone.Dto.V1.Login.Request;
using DocsClone.EfCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DocsClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IConfiguration Configuration { get; }
        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            Configuration = configuration;
        }


        [Route("Login")]

        [HttpPost]
        public ActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var users = _unitOfWork.Users.Find(user => user.Username.ToLower() == loginRequest.Username.ToLower() && user.Password == loginRequest.Password.CreateMD5Hash());
            if (users.Count > 0)
                return Ok(new LoginResponse() { StatusCode = 200, Message = "Login Successful", Token = GenerateJSONWebToken(users.FirstOrDefault()) });
            return BadRequest(new LoginResponse() { StatusCode = 400, Message = "Login Unsuccessful", Token = null });
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            var user = _unitOfWork.Users.Add(new User() { Username = registerRequest.Username, Password = registerRequest.Password.CreateMD5Hash() });
            _unitOfWork.Details.Add(
                new Detail()
                {
                    User = user,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Email = registerRequest.Email,
                    DateOfBirth = registerRequest.DateOfBirth,
                    Name = registerRequest.Name,
                    Surname = registerRequest.Surname,
                    PrimaryContactNumber = registerRequest.PrimaryContactNumber,
                    CreatedWithTimezone = registerRequest.Timezone
                });
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Users()
        {
            return Ok(_unitOfWork.Users.GetAll());
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
              Configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
