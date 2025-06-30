using Backend.Common.Contracts.Base;
using Microsoft.AspNetCore.Mvc;

namespace GrattaEVinci.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [Route("healtcheck")]
        [HttpGet]
        public ActionResult<BaseResponse> HealtCheck()
        {
            BaseResponse response = new BaseResponse();

            try
            {
                response.SetSuccess();
            }
            catch (Exception e)
            {
                //_logger.Error(e.Message);
                response.SetFailure();
                response.Message = e.Message;
            }
            return response;
        }
    }
}
