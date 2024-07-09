using ProjectZ.data.DBRepositories.Login;
using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectZ.Model.Models.CommonModels.Common;

namespace ProjectZ.Services.Services.LoginServices 
{
    public class LoginServ : ILoginServ
    {
        ILoginRepo repo;
        public LoginServ(ILoginRepo repo)
        {
            this.repo = repo;
        }

        public async Task<int> Login(LoginModel model)
        {
            return await repo.Login(model);
        }

        public async Task<LoginResponseModel> LoginAsync(LoginModel model)
        {
            return await repo.LoginAsync(model);
        }

        public async Task<otpmodel> otp(Model.Models.CommonModels.Common.otpmodel model)
        {
            return await repo.otpverification(model);
        }

        public async Task<otpmodel> Resetpassword(Model.Models.CommonModels.Common.otpmodel model)
        {
            return await repo.resetpassword(model);
        }

        public async Task<int> verify(string email)
        {
            return await repo.verify(email);
        }
    }
}
