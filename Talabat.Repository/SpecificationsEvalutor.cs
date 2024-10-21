using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationsEvalutor<T> where T : BaseEntity
    {
        //Func To Build Query
        //_dbContext.Set<T>.Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType);

        public static IQueryable<T> GetQuery(IQueryable<T> InputQuery , ISpecifications<T> specifications)
        {
            var Query = InputQuery;
            if (specifications.Criteria is not null) //P => P.Id == id
            {
                Query = Query.Where(specifications.Criteria); //_dbContext.Set<T>.Where(P => P.Id == id)
            }
            //P => P.ProductBrand  , P => P.ProductType
            Query = specifications.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            //_dbContext.Set<T>.Where(P => P.Id == id).Include(P => P.ProductBrand)
            //_dbContext.Set<T>.Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType)


            return Query;
        }
    }
}
