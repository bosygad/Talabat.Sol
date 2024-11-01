using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        // _dbContext.products.Include(P => P.ProductBrand).Include(P => P.ProductType)

        /// Where(P =>P.Id == id)
        public Expression<Func<T, bool>> Criteria { get; set; }

        ///Include(P => P.ProductBrand)
        public List<Expression<Func<T , object>>> Includes { get; set; }



    }
}
