using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProjectZ.Common.helpers;
using ProjectZ.data.config;
using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data.DBRepositories.Registration
{
    public class RegistrationRepo : BaseRepository, IRegistrationRepo
    {
        IConfiguration _config;
        public RegistrationRepo(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
            _config = config;
        }

        public async Task<Registrationmodel> Registartion(string email)
        {
            var para = new DynamicParameters();
            para.Add("@email", email);
            var data = await QueryFirstOrDefaultAsync<Registrationmodel>(StorageProcedure.registration, para, commandType: CommandType.StoredProcedure);
            if (data != null)
            {
                return new Registrationmodel()
                {
                    Email = email,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Gender = data.Gender,
                    Phonenumber = data.Phonenumber
                };
            }
            else
            {
                return null;
            }
        }


        public async Task<int> Registration(Registrationmodel model)
        {
            var para = new DynamicParameters();
            para.Add("@email", model.Email);
            para.Add("@password", model.Password);
            para.Add("@FirstName", model.FirstName);
            para.Add("@LastName", model.LastName);
            para.Add("@Gender", model.Gender);
            para.Add("@Phonenumber", model.Phonenumber);
            var result = await ExecuteAsync<int>(StorageProcedure.Register, para, commandType: CommandType.StoredProcedure);
            if (result > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
