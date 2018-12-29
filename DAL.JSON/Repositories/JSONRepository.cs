using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DAL.Core;
using DAL.Core.Repositories;
using Domain.Core;

namespace DAL.JSON.Repositories
{
    public class JSONRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppJSONContext _dataContext;
        private readonly List<TEntity> _dataSet;

        public JSONRepository(IDataContext dataContext)
        {
            _dataContext = dataContext as AppJSONContext ?? throw new ArgumentNullException(nameof(dataContext));
            _dataSet = _dataContext.DataSet<TEntity>();
        }

        public virtual IEnumerable<TEntity> All() => _dataSet;

        public virtual async Task<IEnumerable<TEntity>> AllAsync() => await Task.FromResult(All());

        public virtual TEntity Find(params object[] id) => _dataSet.Find(a => a.Id == (int) id[0]);

        public async Task<TEntity> FindAsync(params object[] id) => await Task.FromResult(Find(id));

        public virtual void Add(TEntity entity)
        {
            if (entity.Id == default(int))
            {
                entity.Id = GetNextId();
                _dataSet.Add(entity);
            }
        }

        public virtual Task AddAsync(TEntity entity)
        {
            Add(entity);
            return Task.CompletedTask;
        }

 
        public virtual TEntity Update(TEntity entity)
        {
            var pos = _dataSet.FindIndex(a => a.Id == entity.Id);
            _dataSet[pos] = entity;
            return entity;
        }

        public virtual void Remove(TEntity entity)
        {
            var pos = _dataSet.FindIndex(a => a.Id == entity.Id);
            _dataSet.RemoveAt(pos);
        }

        public virtual void Remove(params object[] id)
        {
            var pos = _dataSet.FindIndex(a => a.Id == (int)id[0]);
            _dataSet.RemoveAt(pos);
        }

        private int GetNextId() => _dataSet.OrderByDescending(a => a.Id).FirstOrDefault()?.Id + 1 ?? 1;
    }
}