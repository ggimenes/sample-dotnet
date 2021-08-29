using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.DDD.Abstractions
{
    public interface IRepository<T>
        where T: IEntity
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Remove(Guid id);
        Task<T> FindById(Guid id);
        Task<IEnumerable<T>> FindAll();
    }
}
