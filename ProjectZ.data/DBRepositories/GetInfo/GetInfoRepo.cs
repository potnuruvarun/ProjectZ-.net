using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProjectZ.Common.helpers;
using ProjectZ.data.config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data.DBRepositories.GetInfo
{
    public class GetInfoRepo : BaseRepository, IGetInfoRepo
    {
        public GetInfoRepo(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
        }

        public async Task<int> GetById(int id)
        {
            var para = new DynamicParameters();
            para.Add("@roleid", id);
            var data = await QueryFirstOrDefaultAsync<int>(StorageProcedure.GetRoled, para, commandType: CommandType.StoredProcedure);
            if(data ==9000 || data==9001)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
