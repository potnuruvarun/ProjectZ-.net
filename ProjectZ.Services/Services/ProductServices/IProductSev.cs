using ProjectZ.Model.Models.CategoryModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Services.Services.ProductServices
{
    public interface IProductSev
    {
        Task<List<Product>> GetAllProducts(Productinput model);
    }
}
