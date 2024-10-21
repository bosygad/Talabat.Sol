using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productrepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> Productrepository , IMapper mapper)
        {
            _productrepository = Productrepository;
            _mapper = mapper;
        }
        ///Get All Product

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()

        {
            var Spec = new ProductWithBrandAndTypeSpecification();
            var Products = await _productrepository.GetAllWithSpecAsync(Spec);
            var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(Products);
            //OkObjectResult result = new OkObjectResult(Products);
            //return result;
            return Ok(MappedProducts);
        }

        ///Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecification(id);
            var Products = await _productrepository.GetByIdWithSpecAsync(Spec);
            var MappedProducts = _mapper.Map<Product,ProductToReturnDTO>(Products);

            return Ok(MappedProducts);
        }

       
    }
}
