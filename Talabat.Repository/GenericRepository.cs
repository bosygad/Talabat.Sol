using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data.BDContext;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region WithOut Spec
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
                return (IEnumerable<T>)await _dbContext.products.Include(P => P.ProductBrand).Include(P => P.ProductType).ToListAsync();

            else
                return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {

            return await _dbContext.Set<T>().FindAsync(id);
            //return await _dbContext.Set<T>.Where(P => P.Id == id).Include(P => P.ProductBrand).Include(P => P.ProductType);
        }

        #endregion

        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecification(specifications).ToListAsync();
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecification(specifications).FirstOrDefaultAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> specifications)
        {
            return SpecificationsEvalutor<T>.GetQuery(_dbContext.Set<T>(), specifications);
        }
    }
}
