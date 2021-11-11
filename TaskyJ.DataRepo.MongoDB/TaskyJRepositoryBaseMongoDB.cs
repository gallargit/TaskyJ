using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
    public abstract class TaskyJRepositoryBaseMongoDB<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private static JsonSerializerSettings jsondefaultsettings = new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            Formatting = Formatting.None
        };

        protected abstract string tablename { get; set; }

        public void Add(T objectToAdd)
        {
            if (objectToAdd.ID == 0)
            {
                objectToAdd.ID = 1;
                var allobjects = GetAll();
                if (allobjects != null && allobjects.Any())
                    objectToAdd.ID += allobjects.Max(x => x.ID);
            }
            MongoDBHelper.Add(tablename, JsonConvert.SerializeObject(objectToAdd, jsondefaultsettings));
        }

        public virtual IEnumerable<T> GetAll()
        {
            var list = MongoDBHelper.GetAll(tablename);
            var listreturn = new List<T>();
            list.ToList().ForEach(i => listreturn.Add(JsonConvert.DeserializeObject<T>(i)));
            return listreturn.OrderBy(x => x.ID);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return GetAll().AsQueryable().Where(predicate.Compile()).ToList();
        }

        public T GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }

        public void Remove(T objectToDelete)
        {
            MongoDBHelper.Remove(tablename, JsonConvert.SerializeObject(objectToDelete));
        }

        public void Update(T objectToUpdate)
        {
            Remove(objectToUpdate);
            Add(objectToUpdate);
        }

        public int Count()
        {
            return GetAll().Count();
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

        public abstract T GetItemInstance();
    }

}
