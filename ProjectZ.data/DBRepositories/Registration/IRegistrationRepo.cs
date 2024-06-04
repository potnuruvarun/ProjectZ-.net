using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data.DBRepositories.Registration
{
    public interface IRegistrationRepo
    {
        Task<int> Registration(Registrationmodel model);
    }
}
