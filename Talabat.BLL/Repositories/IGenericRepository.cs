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
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        #endregion

        #region Within specifications
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specifications);
        Task<T> GetByIdWithSpecAsync(ISpecifications<T> specifications);

        #endregion
    }
}
