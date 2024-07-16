using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProjectZ.Common.helpers;
using ProjectZ.data.config;
using ProjectZ.Model.Models.CategoryModels.Category;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data.DBRepositories.User
{
    public class Productrepo : BaseRepository, IProductrepo
    {
        public Productrepo(IOptions<DataConfig> connectionString, IConfiguration config = null) : base(connectionString, config)
        {
        }

        public async Task<List<Product>> GetProductList(Productinput model)
        {
            var para = new DynamicParameters();
            para.Add("@Category", model.Category);
            para.Add("SubCategory", model.SubCategory);
            var data = await QueryAsync<Product>(StorageProcedure.GetProducts, para, commandType: CommandType.StoredProcedure);
            if (data.Any())
            {
                return data.ToList();   
            }
            else
            {
                return null;
            }
        }
    }
}
