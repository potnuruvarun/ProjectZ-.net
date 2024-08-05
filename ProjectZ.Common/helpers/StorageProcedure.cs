using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Common.helpers
{
    public class StorageProcedure
    {

        #region Login
        public const string registration = "sp_registration";
        public const string Register = "sp_register";
        public const string Login = "sp_login";
        public const string resetpassword = "sp_resetpassword";
        public const string otpmodel = "spotp";
        public const string verify = "sp_verify";
        #endregion


        #region Admin
        public const string posters = "_sp_poster";
        public const string AddingSubCategories = "sp_Subcategory";
        public const string Addproducts = "_spinsertproduct";
        public const string GetRoled = "sp_role";
        public const string DeleteSubCategory = "sp_deletesubcategory";
        public const string DeletePosters = "sp_deleteposters";
        #endregion

        #region Users
        public const string viewposters = "_spposterview";
        public const string viewsubcategoryposters = "sp_viewsubcategoryposters";
        public const string GetProducts = "sp_products";
        public const string GetAllposters = "sp_AllPosters";
        #endregion

    }
}
