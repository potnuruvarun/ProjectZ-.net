using ProjectZ.Model.Models.CategoryModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Services.Services.CategoryServices
{
    public interface ICategoryServ
    {
        Task<List<SubCategory>> AddSubcategory(List<SubCategory> model);

        Task<List<Product>> AddProduct(List<Product> model);
    }
}
