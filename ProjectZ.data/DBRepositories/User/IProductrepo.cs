using ProjectZ.Model.Models.CategoryModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data.DBRepositories.User
{
    public interface IProductrepo
    {
        Task<List<Product>> GetProductList(Productinput model);
    }
}
