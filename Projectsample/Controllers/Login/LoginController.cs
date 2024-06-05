using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectZ.Common.helpers;
using ProjectZ.data.DBRepositories.Registration;
using ProjectZ.Model.Models.RegistrationModels;
using ProjectZ.Services.Services.LoginServices;
using ProjectZ.Services.Services.RegisterServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IRegisterServ services;
        ILoginServ logserv;
        IConfiguration configuration;
        //AppSettings _appSetting;
        public LoginController(IRegistrationRepo repo, IRegisterServ _services, ILoginServ _logiserv, IConfiguration _config)
        {
            services = _services;
            logserv = _logiserv;
            configuration = _config;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> register(Registrationmodel model)
        {
            if (await services.Registration(model) == 1)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ApiPostResponse<LoginResponseModel>> Login([FromBody] LoginModel model)
        {
            ApiPostResponse<LoginResponseModel> Response = new();
            IActionResult response = Unauthorized();
            AuthenticateUser(model);
            LoginResponseModel result = await logserv.LoginAsync(model);
            Response.Data = result;
            if (result != null)
            {
                var tokenString = GenerateJSONWebToken(model);
                result.JWTToken = tokenString;
                Response.Success = true;
                Response.Message = result.ErrorModel.ErrorMsg ?? string.Empty;
            }
            else
            {
                Response.Success = false;
                Response.Message = string.Empty;
            }
            return Response;
        }

        private string GenerateJSONWebToken(LoginModel logindata)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
              new Claim(JwtRegisteredClaimNames.Email, logindata.Email),

            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private LoginModel AuthenticateUser(LoginModel models)
        {
            LoginModel user = null;
            if (models.Email != null)
            {
                user = new LoginModel { Email = models.Email, Password = models.Password };
            }
            return user;
        }

        //[HttpPost("LoginUser")]
        //public async Task<ApiPostResponse<LoginResponseModel>> LoginUser([FromBody] LoginModel model)
        //{
        //    ApiPostResponse<LoginResponseModel> Response = new();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(_appSetting.JWT_Secret)) //checking for JWT_Secret is not null or empty
        //        {
        //            LoginResponseModel Result = await logserv.LoginAsync(model);
        //            Response.Data = Result;
        //            if (Result.Employee.UserId != 0)
        //            {
        //                var jwtToken = _JWTAuthenticationService.GenerateToken(Result.Employee, _appSetting.JWT_Secret, 60);   //token generation

        //                ErrorModel TokenResponse = new();
        //                if (!string.IsNullOrEmpty(jwtToken) && Result.Employee.UserId > 0)
        //                {
        //                    //TokenResponse = await _loginService.UpdateTokenAsync(Result.Employee.UserId, Result.Employee.AgencyId, jwtToken);

        //                    if (TokenResponse.ErrorCode == 100)
        //                    {
        //                        Result.JWTToken = jwtToken;
        //                        Response.Success = true;
        //                        Response.Message = Result.ErrorModel.ErrorMsg ?? string.Empty;

        //                        //Set session value
        //                        HttpContext.Session.SetString("UserName", Result.Employee.UserName ?? string.Empty);
        //                        HttpContext.Session.SetString("UserId", Result.Employee.UserId.ToString());
        //                        HttpContext.Session.SetString("AgencyId", Result.Employee.AgencyId.ToString());
        //                        //HttpContext.Session.SetString("AgencyId", "1");
        //                        HttpContext.Session.SetString("JwtToken", jwtToken);
        //                        HttpContext.Session.SetString("UserTypeId", Result.Employee.UserTypeId.ToString());
        //                        //add more as needed
        //                    }
        //                    else
        //                    {
        //                        Result.ErrorModel = TokenResponse;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Response.Success = false;
        //                Response.Message = Result.ErrorModel.ErrorMsg ?? string.Empty;
        //            }
        //        }
        //        else
        //        {
        //            Response.Success = false;
        //            Response.Message = "Invalid JWT_Secret";
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        string ExceptionMessage = _commonMessages.CreateCommonMessage("LoginUser", Ex.ToString());
        //        _logger.Information(ExceptionMessage);
        //        Response.Success = false;
        //        Response.Message = Ex.Message ?? string.Empty;
        //    }
        //    return Response;
        //}


    }
}
