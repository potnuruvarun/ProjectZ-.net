using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProjectZ.Common.helpers;
using ProjectZ.data.config;
using ProjectZ.data.DBRepositories.GetInfo;
using ProjectZ.Model.Models.CategoryModels.Category;
using ProjectZ.Model.Models.RegistrationModels;
using System.Data;

namespace ProjectZ.data.DBRepositories.Dmeo
{
    
    public class Category : BaseRepository, ICategory
    {
        IGetInfoRepo _inforepo;
        public Category(IOptions<DataConfig> connectionString, IConfiguration config = null,IGetInfoRepo repo=null) : base(connectionString, config)
        {
            _inforepo = repo;
        }
        public async Task<List<SubCategory>> GetSubPosters()
        {
            var data = await QueryAsync<SubCategory>(StorageProcedure.viewsubcategoryposters,commandType: CommandType.StoredProcedure);
            if (data.Count() >= 1)
            {
                return data.ToList();
            }
            return null;
        }

        public async Task<List<Posters>> Add(List<Posters> model)
        {
            var insertedSubCategories = new List<Posters>();

            foreach (var modell in model)
            {

                var category = modell.Category;
                var imagePaths = modell.ImagePath;


                var para = new DynamicParameters();
                para.Add("@CategoryName", category);
                para.Add("@ImagePaths", imagePaths);

                var data = await QueryFirstOrDefaultAsync<Posters>(StorageProcedure.posters, para, commandType: CommandType.StoredProcedure);

                if (data != null)
                {
                    insertedSubCategories.Add(data);
                }
            }

            return insertedSubCategories;

        }

       

        public async Task<List<SubCategory>> AddSubcategory(List<SubCategory> model)
        {
            var insertedSubCategories = new List<SubCategory>();

            foreach (var item in model)
            {
                var category = item.Category;
                var subcategory = item.SubCategoryName;
                var serverpath = item.ImagePath;
                var para = new DynamicParameters();
                para.Add("@Categoryname", category);
                para.Add("@Subcategoryname", subcategory);
                para.Add("@Imagepath", serverpath);

                var data = await QueryFirstOrDefaultAsync<SubCategory>(StorageProcedure.AddingSubCategories, para, commandType: CommandType.StoredProcedure);

                if (data != null)
                {
                    insertedSubCategories.Add(data);
                }
            }

            return insertedSubCategories;
        }

        public async Task<List<Posters>> GetPosters(string Category)
        {
            var para = new DynamicParameters();
            para.Add("@Category", Category);
            var data = await QueryAsync<Posters>(StorageProcedure.viewposters, para, commandType: CommandType.StoredProcedure);
            if (data.Count() >= 1)
            {
                return data.ToList();
            }
            return null;
        }

        public async Task<List<Product>> AddProduct(List<Product> model)
        {

            var insertedSubCategories = new List<Product>();

            foreach (var item in model)
            {
                var category = item.Category;
                var subcategory = item.SubCategoryName;
                var brandname = item.BrandName;
                var Description = item.Description;
                var Amount = item.Amount;
                var Imagepath = item.ImagePath;
                var para = new DynamicParameters();
                para.Add("@Category", category);
                para.Add("@SubCategory", subcategory);
                para.Add("@BrandName", brandname);
                para.Add("@Description", Description);
                para.Add("@Amount", Amount);
                para.Add("@ImagePath", Imagepath);

                var data = await QueryFirstOrDefaultAsync<Product>(StorageProcedure.Addproducts, para, commandType: CommandType.StoredProcedure);

                if (data != null)
                {
                    insertedSubCategories.Add(data);
                }
            }

            return insertedSubCategories;
        }

        public class UserInfo()
        {
            public int roleid { get; set; }
        }

    }

}
