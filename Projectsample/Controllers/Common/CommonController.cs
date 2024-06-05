using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectZ.Common.helpers;
using ProjectZ.Common.Services;
using static ProjectZ.Model.Models.CommonModels.Common;

namespace Api.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        IEmailServices mailserv;

        public CommonController(IEmailServices _mailserv)
        {
            mailserv = _mailserv;
        }

        [HttpPost]
        [Route("Sendmail")]
        public async Task<ApiPostResponse<mailresponse>> Send([FromForm] MailRequest request)
        {
            try
            {
                ApiPostResponse<mailresponse> Response = new();
                 mailresponse result= await mailserv.SendEmailAsync(request);
                if(result!=null)
                {
                    var status = result.status;
                    Response.Success = true;
                    Response.Data = result;
                    Response.Message = result.message;
                }
                else
                {
                    Response.Success = false;
                }
                return Response;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
