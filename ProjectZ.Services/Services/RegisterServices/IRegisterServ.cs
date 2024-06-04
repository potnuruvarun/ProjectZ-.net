using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Services.Services.RegisterServices
{
    public interface IRegisterServ
    {
        Task<int> Registration(Registrationmodel model);

    }
}
