using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProjectZ.Common.helpers;

namespace Api.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController() { }

        public class Employee()
        {

            public string employee_id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string phone_number { get; set; }
            public string position { get; set; }
            public float salary { get; set; }
            public string department { get; set; }
            public DateTime hire_date { get; set; }

        }

        [HttpGet]
        public ApiPostResponse<List<Employee>> Get()
        {
            ApiPostResponse<List<Employee>> Response = new();
            string jsonFile = @"C:\Users\sit363.SIT\source\repos\Projectsample\Projectsample\Employee.json";
            //string json = File.ReadAllText(jsonFile);
            using StreamReader reader = new(jsonFile);
            var json = reader.ReadToEnd();
            var jarray = JArray.Parse(json);
            List<Employee> employeelist = new();
            foreach (var item in jarray)
            {
                Employee data = item.ToObject<Employee>();
                employeelist.Add(data);
                Response.Data = employeelist;
                Response.Success = true;
                //Response.Message = "Error";
            }
            return Response;
        }
    }
}
