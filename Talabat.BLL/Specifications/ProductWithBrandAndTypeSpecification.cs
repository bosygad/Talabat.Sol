using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecifications<Product>
    {
        public ProductWithBrandAndTypeSpecification():base()
        {
            Includes.Add(p => p.ProductType);
            Includes.Add(p => p.ProductBrand);

        }

        /// For Get Product By Id
       public ProductWithBrandAndTypeSpecification(int id) : base(P=>P.Id == id)
        {
            Includes.Add(p => p.ProductType);
            Includes.Add(p => p.ProductBrand);
        }
    }
}
