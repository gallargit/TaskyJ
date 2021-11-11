using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskyJ.Globals.Data.DataObjects;

//Http GET version of the repository, works, but it's not used due to GET limitations - left here as proof of concept
namespace TaskyJ.DataRepo
{
    public abstract class TaskyJRepositoryBaseSTSDBGET<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private string counturl = "/count";
        private string puturl = "/put";
        private string getallurl = "/getall";
        private string geturl = "/get";
        private string removeurl = "/remove";

        protected abstract string tablename { get; set; }

        public void Add(T objectToAdd)
        {
            RequestHelper.sendRequest(Puturl + "?t=" + tablename + "&q=" + JsonConvert.SerializeObject(objectToAdd, new JsonSerializerSettings()
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                TypeNameHandling = TypeNameHandling.All,
                Formatting = Formatting.None
            }));
        }

        public string Baseurl { get; set; } = "http://localhost:7183";

        public string Getallurl
        {
            get
            {
                return Baseurl + getallurl;
            }

            set
            {
                getallurl = value;
            }
        }

        public string Geturl
        {
            get
            {
                return Baseurl + geturl;
            }

            set
            {
                geturl = value;
            }
        }

        public string Removeurl
        {
            get
            {
                return Baseurl + removeurl;
            }

            set
            {
                removeurl = value;
            }
        }

        public string Puturl
        {
            get
            {
                return Baseurl + puturl;
            }

            set
            {
                puturl = value;
            }
        }

        public string Counturl
        {
            get
            {
                return Baseurl + counturl;
            }

            set
            {
                counturl = value;
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            return JsonConvert.DeserializeObject<List<T>>(RequestHelper.sendRequest(Getallurl + "?t=" + tablename),
                new JsonSerializerSettings()
                {
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    TypeNameHandling = TypeNameHandling.All
                });
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return GetAll().AsQueryable().Where(predicate.Compile()).ToList();
        }

        public T GetById(int id)
        {
            string s = RequestHelper.sendRequest(Geturl + "?t=" + tablename + "&id=" + id);
            if (string.IsNullOrEmpty(s) || s == Environment.NewLine)
                return null;
            else
                return JsonConvert.DeserializeObject<T>(s,
                    new JsonSerializerSettings()
                    {
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        TypeNameHandling = TypeNameHandling.All
                    });
        }

        public void Remove(T objectToDelete)
        {
            string s = RequestHelper.sendRequest(Removeurl + "?t=" + tablename + "&id=" + objectToDelete.ID);
        }

        public void Update(T objectToUpdate)
        {
            Remove(objectToUpdate);
            Add(objectToUpdate);
        }

        public int Count()
        {
            string s = RequestHelper.sendRequest(Counturl + "?t=" + tablename).Replace(Environment.NewLine, "");
            int.TryParse(s, out int result);
            return result;
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
