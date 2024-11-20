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
        public ProductWithBrandAndTypeSpecification(string? sort , int? brandId, int? CatId) :base(P=>
            (!brandId.HasValue || P.ProductBrandId == brandId.Value) &&
            (!CatId.HasValue || P.ProductTypeId == CatId.Value)
            )
        {
            Includes.Add(p => p.ProductType);
            Includes.Add(p => p.ProductBrand);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort) 
                {
                    case "PriceAsc":
                        //OrderBy(P=>P.Price)
                        AddOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDesc(P=>P.Price);
                        break;
                    default:
                        AddOrderBy(P=>P.Name);
                        break;


                }
            }
            else
            {
                AddOrderBy(P=> P.Name);
            }
        
        }

        /// For Get Product By Id
       public ProductWithBrandAndTypeSpecification(int id) : base(P=>P.Id == id)
        {
            Includes.Add(p => p.ProductType);
            Includes.Add(p => p.ProductBrand);
        }
    }
}
