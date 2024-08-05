using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProjectZ.data;
using ProjectZ.data.config;
using ProjectZ.data.DBRepositories.User;
using ProjectZ.Model.Models.CategoryModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Services.Services.ProductServices
{
    public class ProductServ : BaseRepository, IProductSev
    {
        IProductrepo repo;
        public ProductServ(IOptions<DataConfig> connectionString, IConfiguration config = null, IProductrepo _repo=null) : base(connectionString, config)
        {
            repo = _repo;
        }

        public async Task<List<Posters>> GetAllPosters()
        {
            return await repo.GetPostersList();
        }

        public async Task<List<Product>> GetAllProducts(Productinput model)
        {
            return await repo.GetProductList(model);
        }
    }
}
