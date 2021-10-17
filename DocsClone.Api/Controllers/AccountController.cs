using DocsClone.Api.SwaggerCustom;
using DocsClone.Domain.Entities;
using DocsClone.Domain.Interfaces;
using DocsClone.Dto.V1.Account.Request;
using DocsClone.Dto.V1.Account.Response;
using DocsClone.Dto.V1.Login.Request;
using DocsClone.Dto.V1.Login.Response;
using DocsClone.EfCore.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Claims;
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
        [SwaggerResponseMimeType((int)HttpStatusCode.OK, typeof(LoginResponse), MediaTypeNames.Application.Json, Description = "Login response with a token")]
        [SwaggerResponseMimeType((int)HttpStatusCode.Forbidden, typeof(LoginResponse), MediaTypeNames.Application.Json, Description = "Login response without a token")]
        public ActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var users = _unitOfWork.Users.Find(user => user.Username.ToLower() == loginRequest.Username.ToLower() && user.Password == loginRequest.Password.CreateMD5Hash());
            if (users.ToList().Count > 0)
                return Ok(new LoginResponse() { StatusCode = (int)HttpStatusCode.OK, Message = "Login Successful", Token = GenerateJSONWebToken(users.FirstOrDefault()) });
            return Unauthorized(new LoginResponse() { StatusCode = (int)HttpStatusCode.Forbidden, Message = "Login Unsuccessful", Token = null });
        }

        [Route("Register")]
        [HttpPost]
        [SwaggerResponseMimeType((int)HttpStatusCode.OK, typeof(RegisterResponse), MediaTypeNames.Application.Json, Description = "Login response with a token")]
        public ActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            var detail=_unitOfWork.Details.Add(
                new Detail()
                {
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Email = registerRequest.Email,
                    DateOfBirth = registerRequest.DateOfBirth,
                    Name = registerRequest.Name,
                    Surname = registerRequest.Surname,
                    PrimaryContactNumber = registerRequest.PrimaryContactNumber,
                    CreatedWithTimezone = registerRequest.Timezone
                });
            _unitOfWork.Users.Add(new User() { Username = registerRequest.Username, Password = registerRequest.Password.CreateMD5Hash(), Detail=detail });
            _unitOfWork.Complete();
            return Ok(new RegisterResponse { Message = "User Registered Successful", StatusCode =(int)HttpStatusCode.OK });
        }

        [Route("Update")]
        [HttpPut]
        [Authorize]
        [SwaggerResponseMimeType((int)HttpStatusCode.OK, typeof(UpdateAccountResponse), MediaTypeNames.Application.Json, Description = "Login response with a token")]
        [SwaggerResponseMimeType((int)HttpStatusCode.BadRequest, typeof(UpdateAccountResponse), MediaTypeNames.Application.Json, Description = "Login response with a token")]
        public ActionResult Update([FromBody] UpdateAccountRequest updateRequest)
        {
            var currentUser = HttpContext.User;
            var tmpUser = _unitOfWork.Users.Find(user => user.Username == currentUser.Claims.FirstOrDefault(c => c.Type == "User").Value).FirstOrDefault();
            if (tmpUser == null)
                return BadRequest(new UpdateAccountResponse() { Message = "Cannot get the user", StatusCode = 200 });
            var tmpDetail = tmpUser.Detail;
            tmpUser.Password = updateRequest.Password.CreateMD5Hash()??tmpUser.Password;
            tmpDetail.Name = updateRequest.Name??tmpDetail.Name;
            tmpDetail.PrimaryContactNumber = updateRequest.PrimaryContactNumber??tmpDetail.PrimaryContactNumber;
            tmpDetail.Surname = updateRequest.Surname??tmpDetail.Surname;
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
