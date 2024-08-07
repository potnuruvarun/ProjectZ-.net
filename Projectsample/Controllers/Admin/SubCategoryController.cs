﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ProjectZ.Common.helpers;
using ProjectZ.data.DBRepositories.Admin;
using ProjectZ.data.DBRepositories.Dmeo;
using ProjectZ.Model.Models.CategoryModels.Category;
using ProjectZ.Services.Services.CategoryServices;
using static ProjectZ.Model.Models.CommonModels.Common;

namespace Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        ICategoryServ services;
        IAdminRepo repo;
        private string _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "SubCategory");
        public SubCategoryController(ICategoryServ _services,IAdminRepo _repo)
        {
            this.services = _services;
            this.repo = _repo;
        }

        [HttpPost("SubcategoryUpload")]
        public async Task<ApiPostResponse<SubCategory>> Upload(IFormFile file)
        {
            ApiPostResponse<SubCategory> Response = new();

            if (file == null || file.Length == 0)
            {
                return Response;
            }

            var dataModels = new List<SubCategory>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var category = worksheet.Cells[row, 2].Text;
                        var subcategory = worksheet.Cells[row, 3].Text;
                        var imagePath = worksheet.Cells[row, 4].Text;

                        var paths = imagePath.Split(',');

                        foreach (var path in paths)
                        {
                            var fileName = Path.GetFileName(path);
                            var imageFolderPath = Path.Combine(_imageFolderPath, category, subcategory);
                            var imageFilePath = Path.Combine(imageFolderPath, fileName);
                            var baseUrl = $"{Request.Scheme}://{Request.Host}";
                            Directory.CreateDirectory(imageFolderPath);
                            System.IO.File.Copy(path, imageFilePath, true);
                            var model = new SubCategory()
                            {
                                SubCategoryName = subcategory,
                                Category = category,
                                ImagePath = $"{baseUrl}/resources/SubCategory/{category}/{subcategory}/{fileName}"
                            };
                            dataModels.Add(model);
                        }
                    }
                }
            }
            var result = await services.AddSubcategory(dataModels);
            if (result != null)
            {
                Response.Success = true;
            }
            else
            {
                Response.Success = false;
                Response.Message = string.Empty;
            }
            return Response;
        }

        [HttpPost("Productupload")]
        public async Task<ApiPostResponse<SubCategory>> productupload(IFormFile file)
        {
            ApiPostResponse<SubCategory> Response = new();

            if (file == null || file.Length == 0)
            {
                return Response;
            }

            var dataModels = new List<Product>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var category = worksheet.Cells[row, 2].Text;
                        var subcategory = worksheet.Cells[row, 3].Text;
                        var BrandName = worksheet.Cells[row, 4].Text;
                        var Description = worksheet.Cells[row, 5].Text;
                        var Amount = worksheet.Cells[row, 6].Text;
                        var ImagePath = worksheet.Cells[row, 7].Text;

                        var paths = ImagePath.Split(',');

                        foreach (var path in paths)
                        {
                            var fileName = Path.GetFileName(path);
                            var imageFolderPath = Path.Combine(_imageFolderPath, category, subcategory);
                            var imageFilePath = Path.Combine(imageFolderPath, fileName);
                            var baseUrl = $"{Request.Scheme}://{Request.Host}";
                            Directory.CreateDirectory(imageFolderPath);
                            System.IO.File.Copy(path, imageFilePath, true);
                            var model = new Product()
                            {
                                SubCategoryName = subcategory,
                                Category = category,
                                ImagePath = $"{baseUrl}/resources/SubCategory/{category}/{subcategory}/{fileName}",
                                Description=Description,
                                BrandName=BrandName,
                                Amount=Convert.ToDecimal(Amount)
                            };
                            dataModels.Add(model);
                        }
                    }
                }
            }
            var result = await services.AddProduct(dataModels);
            if (result != null)
            {
                Response.Success = true;
            }
            else
            {
                Response.Success = false;
                Response.Message = string.Empty;
            }
            return Response;
        }

        [HttpGet]
        public async Task<IActionResult> Deleteallposters()
        {
            var location = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            try
            {
                if (Directory.Exists(location))
                {
                    foreach (var file in Directory.GetFiles(location))
                        try
                        {
                            System.IO.File.Delete(file);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                            return StatusCode(500, $"An error occurred while deleting file {file}.");
                        }
                    foreach (var dir in Directory.GetDirectories(location))
                    {
                        Directory.Delete(dir, true);
                    }

                    return Ok("All files and subdirectories in the Uploads directory have been deleted");
                }
                else
                {
                    return NotFound("Uploads directory not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An error occurred while deleting files.");
            }
        }

        [HttpPost("Category")]
        public async Task <ApiPostResponse<int>>DeleteAllSubcategory(int id)
        {
            ApiPostResponse<int> Response = new();
            var result =await  repo.DeleteallSubCAtegory(id);
            Response.Data = result;
            if(result !=null)
            {
                Response.Success = true;
            }
            else
            {
                Response.Success = false;
                Response.Message = string.Empty;
            }
            return Response;
        }

        [HttpPost("SubCategory")]
        public async Task<ApiPostResponse<int>> DeletePosters(int id)
        {
            ApiPostResponse<int> Response = new();
            var result = await repo.DeletePosters(id);
            Response.Data = result;
            if (result != null)
            {
                Response.Success = true;
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
