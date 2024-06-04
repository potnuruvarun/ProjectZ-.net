using ProjectZ.data.DBRepositories.Registration;
using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Services.Services.RegisterServices
{
    public class RegisterServ : IRegisterServ
    {
        IRegistrationRepo repo;
        public RegisterServ(IRegistrationRepo _repo)
        {
            repo = _repo;
        }
        public async Task<int> Registration(Registrationmodel model)
        {
            return await repo.Registration(model);
        }
    }
}
