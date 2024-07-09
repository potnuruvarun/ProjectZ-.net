using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectZ.Model.Models.CommonModels.Common;

namespace ProjectZ.Services.Services.LoginServices
{
    public interface ILoginServ
    {
        Task<int> Login(LoginModel model);

        Task<LoginResponseModel> LoginAsync(LoginModel model);

        Task<otpmodel> Resetpassword(otpmodel model);
        Task<otpmodel> otp(otpmodel model);

        Task<int> verify(string email);

    }
}
