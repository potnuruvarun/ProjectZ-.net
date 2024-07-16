using ProjectZ.Services.Services.CategoryServices;
using ProjectZ.Services.Services.LoginServices;
using ProjectZ.Services.Services.ProductServices;
using ProjectZ.Services.Services.RegisterServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Services
{
    public class ServiceRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dictionary = new Dictionary<Type, Type>()
            {
                { typeof(IRegisterServ),typeof(RegisterServ)},
                { typeof(ILoginServ),typeof(LoginServ)},
                { typeof(ICategoryServ),typeof(CategoryServ)},
                { typeof(IProductSev),typeof(ProductServ)}
            };
            return dictionary;
        }
    }
}
