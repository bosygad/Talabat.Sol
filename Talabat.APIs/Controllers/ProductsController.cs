using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : APIBaseController
    {
        private readonly IGenericRepository<Product> _productrepository;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;

        public ProductsController(IGenericRepository<Product> Productrepository 
                                 , IMapper mapper
                                 ,IGenericRepository<ProductType> TypeRepo
                                 ,IGenericRepository<ProductBrand> BrandRepo)
        {
            _productrepository = Productrepository;
            _mapper = mapper;
            _typeRepo = TypeRepo;
            ///test
            _brandRepo = BrandRepo;
        }
        ///Get All Product

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string sort)

        {

            var Spec = new ProductWithBrandAndTypeSpecification(sort);
            var Products = await _productrepository.GetAllWithSpecAsync(Spec);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(Products);
            //OkObjectResult result = new OkObjectResult(Products);
            //return result;
            return Ok(MappedProducts);
        }

        ///Get Product By Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecification(id);
            var Products = await _productrepository.GetByIdWithSpecAsync(Spec);
            if (Products is null) { return NotFound(new ApiResponse(404)); }
            var MappedProducts = _mapper.Map<Product,ProductToReturnDTO>(Products);
            return Ok(MappedProducts);
            
        }


        //Get All Cat 
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes() 
        {
            var Types = await _typeRepo.GetAllAsync();
            return Ok(Types);

        }

        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var Brands = await _brandRepo.GetAllAsync();
            return Ok(Brands);
        }
    }
}
