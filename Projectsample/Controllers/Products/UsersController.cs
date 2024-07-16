using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectZ.Common.helpers;
using ProjectZ.data.DBRepositories.Dmeo;
using ProjectZ.data.DBRepositories.GetInfo;
using ProjectZ.Model.Models.CategoryModels.Category;
using ProjectZ.Services.Services.ProductServices;

namespace Api.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ICategory _repo;
        IProductSev serv;

        public UsersController(ICategory demo,IProductSev services)
        {
            _repo = demo;
            serv = services;
        }

        [HttpGet("GetPosters")]
        public async Task<ApiPostResponse<List<Posters>>> Getdaata(string Category)
        {
            ApiPostResponse<List<Posters>> Response = new();
            var result = await _repo.GetPosters(Category);
            Response.Data = result;
            if (result != null)
            {
                Response.Success = true;
                Response.Message = "Success";
            }
            else
            {
                Response.Success = false;
                Response.Message = string.Empty;
            }
            return Response;
        }

        [HttpGet("GetSubCAtegoryPosters")]
        public async Task<ApiPostResponse<List<SubCategory>>> GetSubcategoryposters(string Category)
        {
            ApiPostResponse<List<SubCategory>> Response = new();
            var result = await _repo.GetSubPosters(Category);
            Response.Data = result;
            if (result != null)
            {
                Response.Success = true;
                Response.Message = "Success";
            }
            else
            {
                Response.Success = false;
                Response.Message = string.Empty;
            }
            return Response;
        }

        [HttpGet("Products")]
        public async Task<ApiPostResponse<List<Product>>>GetProducts(Productinput model)
        {
            ApiPostResponse<List<Product>> Response = new();
            var result = await serv.GetAllProducts(model);
            Response.Data = result;
            if (result != null)
            {
                Response.Success = true;
                Response.Message = "Success";
            }
            else
            {
                Response.Success = false;
                Response.Message = string.Empty;
            }
            return Response;
        }

    }
}
