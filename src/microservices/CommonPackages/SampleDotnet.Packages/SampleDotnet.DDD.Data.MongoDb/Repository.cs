using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.DDD.Data.MongoDb
{
    public class Repository<T> : IRepository<T>
        where T : IEntity
    {
        public Task Add(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
