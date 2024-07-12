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
    }

    public class SubCategory
    {
        public string? SubCategoryName { get; set; }
        public string? Category { get; set; }
        public string? ImagePath { get; set; }
    }
}
    