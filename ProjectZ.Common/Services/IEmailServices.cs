using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectZ.Model.Models.CommonModels.Common;

namespace ProjectZ.Common.Services
{
    public interface IEmailServices
    {
        Task<mailresponse> SendEmailAsync(MailRequest mailRequest);

        Task SendEmail(string toEmail, string subject, string body);
    }
}
