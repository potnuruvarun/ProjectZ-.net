using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using ProjectZ.data;
using ProjectZ.Services;
using static ProjectZ.Model.Models.CommonModels.Common;

namespace Api
{
    public static class RegistartionServices
    {
        public static void RegisterService(this IServiceCollection services)
        {

            Configure(services, DataRegister.GetTypes());
            Configure(services, ServiceRegister.GetTypes());
        }
        public static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (var type in types)
            {
                services.AddScoped(type.Key, type.Value);
            }
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddMvc();
            services.AddHttpContextAccessor();
        }
      
    }
}
