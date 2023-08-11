using Ecommerce_Angular_DotNetAPI.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Angular_DotNetAPI.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
