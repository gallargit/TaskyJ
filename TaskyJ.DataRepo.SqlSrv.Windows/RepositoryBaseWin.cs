using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
    public abstract class RepositoryBaseWin<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected TaskyJEntities dbConn = new TaskyJEntities();

        public abstract void Add(T objectToAdd);
        public abstract void Remove(T objectToDelete);
        public abstract void Update(T objectToUpdate);
        public abstract IEnumerable<T> GetAll();
        public abstract T GetById(int id);
        public abstract IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        public int Count()
        {
            return GetAll().Count();
        }

        public T GetItemInstance()
        {
            object o = new TaskyJTable();
            return (T)o;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

    }
}
