using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
    public interface IRepositoryBase<T> : IDisposable where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        void Add(T objectToAdd);
        void Remove(T objectToDelete);
        void Update(T objectToUpdate);
        int Count();
        T GetItemInstance();
    }
}
