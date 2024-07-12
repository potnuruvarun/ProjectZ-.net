using ProjectZ.data.DBRepositories.Dmeo;
using ProjectZ.Model.Models.CategoryModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Services.Services.CategoryServices
{
    public class CategoryServ : ICategoryServ
    {
        ICategory repo;
        public CategoryServ(ICategory _repo)
        {
            repo = _repo;
        }
        public async Task<List<SubCategory>> AddSubcategory(List<SubCategory> model)
        {
            return await repo.AddSubcategory(model);
        }
    }
}
