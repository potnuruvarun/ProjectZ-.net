using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProjectZ.Common.helpers;
using ProjectZ.data.config;
using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data.DBRepositories.Login
{
    public class LoginRepo : BaseRepository, ILoginRepo
    {
        IConfiguration _config;
        public LoginRepo(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
            _config = config;
        }

        public async Task<int> Login(LoginModel model)
        {
            var para = new DynamicParameters();
            para.Add("@Email", model.Email);
            para.Add("@Password", model.Password);
            var data = await QueryFirstOrDefaultAsync<int>(StorageProcedure.Login, para, commandType: CommandType.StoredProcedure);
            if (data > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<LoginResponseModel> LoginAsync(LoginModel model)
        {
            var para = new DynamicParameters();
            para.Add("@Email", model.Email);
            para.Add("@Password", model.Password);
            var data = await QueryFirstOrDefaultAsync<LoginResponseModel>(StorageProcedure.Login, para, commandType: CommandType.StoredProcedure);
            if(data!=null)
            {
            return data;
            }
            return null;
        }
    }
}
