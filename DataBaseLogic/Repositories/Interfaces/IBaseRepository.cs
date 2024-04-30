using DataBaseLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseLogic.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetData(CancellationToken? cancellation = null, Expression<Func<T, bool>> predicate = null);
    }
}
