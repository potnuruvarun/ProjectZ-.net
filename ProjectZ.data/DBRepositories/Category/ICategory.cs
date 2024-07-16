using ProjectZ.Model.Models.CategoryModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectZ.Model.Models.CommonModels.Common;

namespace ProjectZ.data.DBRepositories.Dmeo
{
    public interface ICategory
    {
        Task<List<Posters>> Add(List<Posters> model);
        Task<List<Posters>> GetPosters(string Category);
        Task<List<SubCategory>>AddSubcategory(List<SubCategory> model);

        Task<List<SubCategory>> GetSubPosters(string Category);

        Task<List<Product>> AddProduct(List<Product> model);
    }
}
