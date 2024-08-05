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

namespace ProjectZ.data.DBRepositories.Admin
{
    public class Adminrepo : BaseRepository, IAdminRepo
    {
        public Adminrepo(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
        }

        public async Task<int> DeleteallSubCAtegory(int id)
        {
            var para = new DynamicParameters();
            para.Add("@SubcategoryId", id);
            var data=await ExecuteAsync<int>(StorageProcedure.DeleteSubCategory, para,commandType:CommandType.StoredProcedure);
            if(data!=null)
            {
                return 100;
            }
            else
            {
                return 400;
            }
        }
        public async Task<int> DeletePosters(int id)
        {
            var para = new DynamicParameters();
            para.Add("@Id", id);
            var data = await ExecuteAsync<int>(StorageProcedure.DeleteSubCategory, para, commandType: CommandType.StoredProcedure);
            if (data != null)
            {
                return 100;
            }
            else
            {
                return 400;
            }
        }
    }
}
