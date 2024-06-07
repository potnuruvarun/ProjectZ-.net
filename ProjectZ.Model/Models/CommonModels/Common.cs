using Microsoft.AspNetCore.Http;
using ProjectZ.Model.Models.RegistrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectZ.Model.Models.CommonModels
{
    public class Common
    {
        public class MailRequest
        {
            public string ToEmail { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public List<IFormFile> Attachments { get; set; }
        }

        public class MailSettings
        {
            public string Server { get; set; }
            public int Port { get; set; }
            public string SenderName { get; set; }
            public string SenderEmail { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Mail { get; set; }
            public string DisplayName { get; set; }
            public string Host { get; set; }
        }

        public class mailresponse
        {
            public string? message { get; set; }
            public string? status { get; set; }
            public MailRequest mail { get; set; }
        }


        public class otpmodel
        {
            public string? email { get; set; }
            public int otp { get; set; }
            public string? password { get; set; }
        }
    }




}
