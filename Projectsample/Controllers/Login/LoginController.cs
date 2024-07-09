using Api.JwtFeatures;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectZ.Common.helpers;
using ProjectZ.Common.Services;
using ProjectZ.data;
using ProjectZ.data.DBRepositories.Registration;
using ProjectZ.Model.Models.RegistrationModels;
using ProjectZ.Services.Services.LoginServices;
using ProjectZ.Services.Services.RegisterServices;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using static ProjectZ.Model.Models.CommonModels.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Api.Controllers.Login
{
    [Route("api/[controller]")]
    //[EnableCors("two_factor_auth_cors")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IRegisterServ services;
        ILoginServ logserv;
        IConfiguration configuration;
        IEmailServices mailsev;
        private  UrlEncoder _urlEncoder;
        private  UserManager<User> _userManager;
        private const string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
        private IMapper _mapper;
        //private  JwtHandler _jwtHandler;
        //AppSettings _appSetting;
        public LoginController(IRegistrationRepo repo, IRegisterServ _services, ILoginServ _logiserv, IConfiguration _config, IEmailServices _mailserv,UserManager<User> manager,UrlEncoder _url/*,JwtHandler _jwt*/)
        {
            services = _services;
            logserv = _logiserv;
            configuration = _config;
            mailsev = _mailserv;
            _userManager = manager;
            _urlEncoder = _url;
            //_jwtHandler=_jwt;
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

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            //var user = _mapper.Map<User>(userForRegistration);
            var user = new User()
            {
                UserName=userForRegistration.FirstName,
                FirstName = userForRegistration.FirstName,
                LastName = userForRegistration.LastName,
                Email=userForRegistration.Email,
            };

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            return Ok();
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
            if (result != null && result.Active==1)
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

        private int GenerateRandomOtp()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999); // Generates a random 6-digit OTP
        }

        [HttpPost]
        [Route("{email}")]
        public async Task<ApiPostResponse<otpmodel>> SendOtp(string email)
        {
            ApiPostResponse<otpmodel> Response = new();
            var otp = GenerateRandomOtp();
            var model = new otpmodel
            {
                email = email,
                otp = otp
            };

            // Send the OTP via email
            var subject = "Your One-Time Password (OTP)";
            var body = $"Your OTP is: {otp}";
            otpmodel result = await logserv.otp(model);

            Response.Data = result;
            if (result != null)
            {
                await mailsev.SendEmail(email, subject, body);
                Response.Success = true;
                Response.Message = "OTP success";
            }
            else
            {
                Response.Success = false;
            }
            return Response;
        }

        [HttpGet("tfa-setup")]
        public async Task<IActionResult> GetTfaSetup(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
                return BadRequest("User does not exist");

            var isTfaEnabled = await _userManager.GetTwoFactorEnabledAsync(user);

            var authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (authenticatorKey == null)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                authenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }
            var formattedKey = GenerateQRCode(email, authenticatorKey);

            return Ok(new TfaSetupDto
            { IsTfaEnabled = isTfaEnabled, AuthenticatorKey = authenticatorKey, FormattedKey = formattedKey });
        }
        private string GenerateQRCode(string email, string unformattedKey)
        {
            return string.Format(
            AuthenticatorUriFormat,
                _urlEncoder.Encode("Code Maze Two-Factor Auth"),
                _urlEncoder.Encode(email),
                unformattedKey);
        }

        [HttpPost("tfa-setup")]
        public async Task<IActionResult> PostTfaSetup([FromBody] TfaSetupDto tfaModel)
        {
            var user = await _userManager.FindByNameAsync(tfaModel.Email);

            var isValidCode = await _userManager
                .VerifyTwoFactorTokenAsync(user,
                  _userManager.Options.Tokens.AuthenticatorTokenProvider,
                  tfaModel.Code);

            if (isValidCode)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true);
                return Ok(new TfaSetupDto { IsTfaEnabled = true });
            }
            else
            {
                return BadRequest("Invalid code");
            }
        }

        [HttpDelete("tfa-setup")]
        public async Task<IActionResult> DeleteTfaSetup(string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);

            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            else
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
                return Ok(new TfaSetupDto { IsTfaEnabled = false });
            }
        }


        [HttpPost]
        [Route("reset")]
        public async Task<ApiPostResponse<otpmodel>> reset(otpmodel models)
        {
            ApiPostResponse<otpmodel> Response = new();
            otpmodel result = await logserv.Resetpassword(models);
            Response.Data = result;
            if (result != null)
            {
                Response.Success = true;

            }

            else
            {
                Response.Success = false;
            }
            return Response;
        }

        [HttpGet("Verify")]
        public async Task <ApiPostResponse<string>> Verify(string email)
        {
            ApiPostResponse<string> Response = new();
            var Data = await logserv.verify(email);
            string result = Data.ToString();
            if (Data == 1)
            {
                Response.Success = true;
            }
            else
            {
                Response.Success = false;
            }
            return Response;

        }
        //[HttpPost("loginauth")]
        //public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
        //{
        //    var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

        //    if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
        //        return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

        //    var isTfaEnabled = await _userManager.GetTwoFactorEnabledAsync(user);

        //    if (!isTfaEnabled)
        //    {
        //        var signingCredentials = _jwtHandler.GetSigningCredentials();
        //        var claims = _jwtHandler.GetClaims(user);
        //        var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
        //        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        //        return Ok(new AuthResponseDto { IsAuthSuccessful = true, IsTfaEnabled = false, Token = token });
        //    }

        //    return Ok(new AuthResponseDto { IsAuthSuccessful = true, IsTfaEnabled = true });
        //}

        //[HttpPost("login-tfa")]
        //public async Task<IActionResult> LoginTfa([FromBody] TfaDto tfaDto)
        //{
        //    var user = await _userManager.FindByNameAsync(tfaDto.Email);

        //    if (user == null)
        //        return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });

        //    var validVerification = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, tfaDto.Code);
        //    if (!validVerification)
        //        return BadRequest("Invalid Token Verification");

        //    var signingCredentials = _jwtHandler.GetSigningCredentials();
        //    var claims = _jwtHandler.GetClaims(user);
        //    var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
        //    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        //    return Ok(new AuthResponseDto { IsAuthSuccessful = true, IsTfaEnabled = true, Token = token });
        //}
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
    public class TfaSetupDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public bool IsTfaEnabled { get; set; }
        public string? AuthenticatorKey { get; set; }
        public string? FormattedKey { get; set; }
    }
    public class UserForRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class RegistrationResponseDto
    {
        public bool IsSuccessfulRegistration { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }

    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public bool IsTfaEnabled { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
    public class TfaDto
    {
        public string? Email { get; set; }
        public string? Code { get; set; }
    }

}
