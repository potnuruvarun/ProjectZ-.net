using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Services.Services.LoginServices
{
    public interface ILoginServ
    {
        Task<int> Login(LoginModel model);

        Task<LoginResponseModel>LoginAsync(LoginModel model);
    }
}
