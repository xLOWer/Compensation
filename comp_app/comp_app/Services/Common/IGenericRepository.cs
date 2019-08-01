using comp_app.MVVM.Model.Common;
using System;
using System.Collections.Generic;

namespace comp_app.Services.Common
{
    public interface IGenericRepository<TEntity> where TEntity : IRef
    {
        void Create(TEntity item, Func<TEntity, bool> callback);
        void Remove(TEntity item, Func<string, bool> callback);
        void Update(TEntity item, Func<TEntity, bool> callback);
        IEnumerable<TEntity> GetAll();

        TEntity FindById(string id);
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
    }
}
