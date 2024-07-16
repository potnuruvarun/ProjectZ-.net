using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Model.Models.CategoryModels.Category
{
    public class Posters
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? ImagePath { get; set; }

        public string? ServerPath { get; set; }
        //public int roleid { get; set; }
    }

    public class SubCategory
    {
        public string? SubCategoryName { get; set; }
        public string? Category { get; set; }
        public string? ImagePath { get; set; }
    }

    public class Product
    {
        public string? Category { get; set; }
        public string? SubCategoryName { get; set; }
        public string? BrandName { get; set; }

        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public string? ImagePath { get; set; }
    }

    public class Productinput
    {
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
    }
}
