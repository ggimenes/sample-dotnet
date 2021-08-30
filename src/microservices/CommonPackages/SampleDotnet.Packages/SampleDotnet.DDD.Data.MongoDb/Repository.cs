using MongoDB.Driver;
using MongoDbGenericRepository;
using MongoDbGenericRepository.Models;
using SampleDotnet.DDD.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleDotnet.DDD.Data.MongoDb
{
    public class MongoRepository<T> : BaseMongoRepository, IRepository<T>
        where T : IEntity, IDocument
    {
        public MongoRepository(IMongoDatabase databaseBase)
            :base(databaseBase)
        {

        }

        public MongoRepository(string connectionString, string databaseName = null) : base(connectionString, databaseName)
        {
        }

        public Task Add(T entity)
        {
            return base.AddOneAsync<T>(entity);
        }

        public Task<IEnumerable<T>> FindAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> FindById(Guid id)
        {
            return base.GetOneAsync<T>(x => ((IDocument)x).Id == id);
        }

        public Task Remove(Guid id)
        {
            return base.DeleteOneAsync<T>(x => ((IDocument)x).Id == id);
        }

        public Task Update(T entity)
        {
            return base.UpdateOneAsync<T>(entity);
        }
    }
}
