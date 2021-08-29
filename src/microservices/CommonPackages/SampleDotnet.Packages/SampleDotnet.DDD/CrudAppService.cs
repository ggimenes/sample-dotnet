using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.DDD
{
    public class CrudAppService<T> : ICrudAppService<T>
        where T : IEntity
    {
        private readonly IRepository<T> _repository;

        public CrudAppService(IRepository<T> repository)
        {
            this._repository = repository;
        }

        public virtual Task Add(T entity)
        {
            return _repository.Add(entity);
        }
        public virtual Task Update(T entity)
        {
            return _repository.Update(entity);
        }
        public virtual Task Remove(Guid id)
        {
            return _repository.Remove(id);
        }
        public virtual Task<T> FindById(Guid id)
        {
            return _repository.FindById(id);
        }
        public virtual Task<IEnumerable<T>> FindAll()
        {
            return _repository.FindAll();
        }
    }
}
