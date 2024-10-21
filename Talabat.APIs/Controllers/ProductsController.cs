using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productrepository;

        public ProductsController(IGenericRepository<Product> Productrepository)
        {
            _productrepository = Productrepository;
        }
        ///Get All Product

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Products = await _productrepository.GetAllAsync();
            //OkObjectResult result = new OkObjectResult(Products);
            //return result;
            return Ok(Products);
        }

        ///Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var Product = await _productrepository.GetByIdAsync(id);
            return Ok(Product);
        }

       
    }
}
