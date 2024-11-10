using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data.BDContext;

namespace Talabat.APIs.Controllers
{
   
    public class BuggyController : APIBaseController
    {
        private readonly StoreDbContext _dbContext;

        public BuggyController(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var Product = _dbContext.products.Find(100);
            if (Product is null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(Product);
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerError() 
        {
            var Product = _dbContext.products.Find(100);
            var ProductReturn = Product.ToString();
            return Ok(ProductReturn);
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();

        }

        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();

        }

    }
}
