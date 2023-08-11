using Ecommerce_Angular_DotNetAPI.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Angular_DotNetAPI.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly AppDbContext db;

        public BuggyController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = db.Products.Find(42);
            if(thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(thing);
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = db.Products.Find(42);
            var thingToReturn = thing.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}
