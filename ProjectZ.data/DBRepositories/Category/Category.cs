using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProjectZ.Common.helpers;
using ProjectZ.data.config;
using ProjectZ.Model.Models.CategoryModels.Category;
using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data.DBRepositories.Dmeo
{
    
    public class Category : BaseRepository, ICategory
    {
        IMapper _mapper;
        public Category(IOptions<DataConfig> connectionString, IConfiguration config = null, IMapper mapper = null) : base(connectionString, config)
        {
            _mapper = mapper;
        }
        public async Task<List<SubCategory>> GetSubPosters(string Category)
        {
            var para = new DynamicParameters();
            para.Add("@subcategoryposters", Category);
            var data = await QueryAsync<SubCategory>(StorageProcedure.viewsubcategoryposters, para, commandType: CommandType.StoredProcedure);
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
                var serverpath = modell.ServerPath;


                var para = new DynamicParameters();
                para.Add("@CategoryName", category);
                para.Add("@ImagePaths", imagePaths);
                para.Add("@Serverpath", serverpath);

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
                var serverpath = item.ServerPath;
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

      


    }

}
