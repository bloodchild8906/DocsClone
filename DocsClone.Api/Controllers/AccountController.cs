using DocsClone.Domain.Entities;
using DocsClone.Domain.Interfaces;
using DocsClone.Dto.V1.Account.Request;
using DocsClone.Dto.V1.Account.Response;
using DocsClone.Dto.V1.Login.Request;
using DocsClone.Dto.V1.Login.Response;
using DocsClone.EfCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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

        [Route("Update")]
        [HttpPost]
        [Authorize]
        public ActionResult Update([FromBody] UpdateAccountRequest updateRequest)
        {
            var currentUser = HttpContext.User;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value).FirstOrDefault();
            if (tmpUser == null)
                return BadRequest(new UpdateAccountResponse() { Message = "Cannot get the user", StatusCode = 200 });
            var tmpDetail = _unitOfWork.Details.Find(detail => detail.User == tmpUser).FirstOrDefault();
            tmpUser.Password = updateRequest.Password.CreateMD5Hash()??tmpUser.Password;
            tmpDetail.Name = updateRequest.Name??tmpDetail.Name;
            tmpDetail.PrimaryContactNumber = updateRequest.PrimaryContactNumber??tmpDetail.PrimaryContactNumber;
            tmpDetail.Surname = updateRequest.Surname??tmpDetail.Surname;
            tmpDetail.User = tmpUser??tmpDetail.User;
            tmpDetail.ModifiedWithTimezone = updateRequest.Timezone ?? tmpDetail.ModifiedWithTimezone;
            tmpDetail.DateModified = DateTime.Now;
            tmpDetail.Email = updateRequest.Email??tmpDetail.Email;
            tmpDetail.DateOfBirth = updateRequest.DateOfBirth ?? tmpDetail.DateOfBirth;
            _unitOfWork.Users.Update(tmpUser);
            _unitOfWork.Details.Update(tmpDetail);
            _unitOfWork.Complete();
            return Ok(new UpdateAccountResponse() { Message = "Update Successful", StatusCode = 200 });
        }



        private string GenerateJSONWebToken(User userEntity)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("User",userEntity.Username)
            };

            var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                Configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
