using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        #region Without specifications
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        #endregion

        #region Within specifications
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> specifications);
        Task<T> GetByIdWithSpecAsync(ISpecifications<T> specifications);

        #endregion
    }
}
