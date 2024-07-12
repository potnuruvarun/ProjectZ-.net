using ProjectZ.data.DBRepositories.Dmeo;
using ProjectZ.data.DBRepositories.Login;
using ProjectZ.data.DBRepositories.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data
{
    public class DataRegister
    {
        public static Dictionary<Type, Type> GetTypes()
        {
            var dictionary = new Dictionary<Type, Type>()
            {
                { typeof(IRegistrationRepo),typeof(RegistrationRepo)},
                {typeof(ILoginRepo),typeof(LoginRepo)},
                {typeof(ICategory),typeof(Category)},

            };
            return dictionary;
        }
    }
}
