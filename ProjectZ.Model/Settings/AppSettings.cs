using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.data.Settings
{
    public class AppSettings
    {
        public string? JWT_Secret { get; set; }
        public int JWT_Validity_Mins { get; set; }
        public int PasswordLinkValidityMins { get; set; }
        public string? AESDecryptionKey { get; set; }
        public string? AESIVKey { get; set; }
        public string? APIURL { get; set; }
        public string? FrontPortalURL { get; set; }
        public string? AdminPortalURL { get; set; }
        public int ForgotPasswordAttemptValidityMin { get; set; }
        public int ForgotPasswordAttemptMaxCountPerHour { get; set; }
        public string? FileNameDateTimeFormat { get; set; }
    }
}
