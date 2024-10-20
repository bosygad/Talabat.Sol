using Talabat.Core.Entities;
using Talabat.Core.Repository;

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
        ///Get Product By Id
    }
}
