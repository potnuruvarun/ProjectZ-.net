//using AutoMapper;
//using ProjectZ.Model.Models.CategoryModels.Category;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ProjectZ.data
//{
//    public class DtoProfile:Profile
//    {
//        public DtoProfile()
//        {
//            CreateMap<DbSubCategory, SubCategory>()
//            .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.SubCategoryName))
//            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new Posters
//            {
//                ID = src.PosterID,
//                Category = src.PosterCategory,
//                ImagePath = src.PosterImagePath
//            }));
//        }
//    }
//}
