using ProjectZ.data.DBRepositories.Login;
using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
