using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using comp_app.MVVM.Model.Common;

namespace comp_app.Services.Common
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : IRef
    {
        IList<TEntity> list;

        public GenericRepository(string SelectSql)
        {            
            list = DbService<TEntity>.DocumentSelect(SelectSql);
        }

        public GenericRepository(IList<TEntity> _list)
        {
            list = _list;
        }

        public void Create(TEntity item, Func<TEntity, bool> callback)
        {            
            if(callback.Invoke(item)) list.Add(item);
        }

        public void Remove(TEntity item, Func<string, bool> callback)
        {
            if(callback.Invoke(item.Id)) list.Remove(item);
        }

        public void Update(TEntity item, Func<TEntity, bool> callback)
        {
            callback.Invoke(item);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return list.ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return list.Where(predicate).ToList();
        }

        public TEntity FindById(string id)
        {
            return list.First(x => x.Id == id);
        }
    }
}
