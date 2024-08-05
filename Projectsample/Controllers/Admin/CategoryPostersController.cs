using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ProjectZ.Common.helpers;
using ProjectZ.data;
using ProjectZ.data.DBRepositories.Dmeo;
using ProjectZ.data.DBRepositories.GetInfo;
using ProjectZ.Model.Models.CategoryModels.Category;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using static ProjectZ.data.RepositoryContext;
using static ProjectZ.Model.Models.CommonModels.Common;

namespace Api.Controllers.Collection
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryPostersController : ControllerBase
    {
        private string _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Posters");
        ICategory _repo;
        IGetInfoRepo _infoRepo;

        public CategoryPostersController(ICategory demo, IGetInfoRepo inforepo)
        {
            _repo = demo;
            _infoRepo= inforepo;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Resources");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost("upload")]
        public async Task<ApiPostResponse<Posters>> Upload(IFormFile file)
        {
            ApiPostResponse<Posters> Response = new();

            if (file == null || file.Length == 0)
            {
                return Response;
            }

            var dataModels = new List<Posters>();
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
                        var imagePath = worksheet.Cells[row, 3].Text;

                        var paths = imagePath.Split(',');

                        foreach (var path in paths)
                        {
                            var fileName = Path.GetFileName(path);
                            var imageFolderPath = Path.Combine(_imageFolderPath, category + "Posters");
                            var imageFilePath = Path.Combine(imageFolderPath, fileName);
                            var baseUrl = $"{Request.Scheme}://{Request.Host}";
                            Directory.CreateDirectory(imageFolderPath);
                            System.IO.File.Copy(path, imageFilePath, true);
                            var model = new Posters()
                            {
                                Category = category,
                                ImagePath = $"{baseUrl}/resources/Posters/{category}Posters/{fileName}",
                            };
                            dataModels.Add(model);
                        }
                    }
                }
            }
            var result = await _repo.Add(dataModels);
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