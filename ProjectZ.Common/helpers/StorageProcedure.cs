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

        #region Registrtaion
        public const string registration = "sp_registration";
        public const string Register = "sp_register";
        public const string Login = "sp_login";
        public const string resetpassword = "sp_resetpassword";
        public const string otpmodel = "spotp";
        public const string verify = "sp_verify";
        #endregion


        #region
        public const string posters = "_sp_poster";
        public const string viewposters = "_spposterview";
        #endregion

        #region
        public const string AddingSubCategories = "sp_Subcategory";
        public const string viewsubcategoryposters = "sp_viewsubcategoryposters";

        #endregion

    }
}
