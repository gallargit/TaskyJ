using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskyJ.Globals.Data.DataObjects;

//Http POST version of the repository
namespace TaskyJ.DataRepo
{
    public abstract class TaskyJRepositoryBaseSTSDB<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private string counturl = "/count";
        private string puturl = "/put";
        private string getallurl = "/getall";
        private string geturl = "/get";
        private string removeurl = "/remove";
        private static JsonSerializerSettings jsondefaultsettings = new JsonSerializerSettings()
        {
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.None
        };

        protected abstract string tablename { get; set; }

        public virtual void Add(T objectToAdd)
        {
            var postvalues = new Dictionary<string, string>();
            postvalues.Add("t", tablename);
            postvalues.Add("q", JsonConvert.SerializeObject(objectToAdd, jsondefaultsettings));
            RequestHelper.sendRequest(Puturl, JsonConvert.SerializeObject(postvalues, jsondefaultsettings));
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
            var postvalues = new Dictionary<string, string>()
            {
                { "t", tablename }
            };
            var list = RequestHelper.sendRequest(Getallurl, JsonConvert.SerializeObject(postvalues, jsondefaultsettings));
            return JsonConvert.DeserializeObject<List<T>>(list);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return GetAll().AsQueryable().Where(predicate.Compile()).ToList();
        }

        public T GetById(int id)
        {
            var postvalues = new Dictionary<string, string>()
            {
                { "t", tablename },
                { "id", id.ToString() }
            };

            string s = RequestHelper.sendRequest(Geturl, JsonConvert.SerializeObject(postvalues, jsondefaultsettings));
            if (s == "error Unable to connect to the remote server")
            {
                throw new Exception(s);
            }
            return string.IsNullOrEmpty(s) || s == Environment.NewLine || s.StartsWith("error ")
                ? null
                : JsonConvert.DeserializeObject<T>(s,
                    new JsonSerializerSettings()
                    {
                        DateFormatHandling = DateFormatHandling.IsoDateFormat,
                        TypeNameHandling = TypeNameHandling.All
                    });
        }

        public void Remove(T objectToDelete)
        {
            var postvalues = new Dictionary<string, string>()
            {
                { "t", tablename },
                { "id", objectToDelete.ID.ToString() }
            };
            string s = RequestHelper.sendRequest(Removeurl, JsonConvert.SerializeObject(postvalues, jsondefaultsettings));
        }

        public void Update(T objectToUpdate)
        {
            Remove(objectToUpdate);
            Add(objectToUpdate);
        }

        public int Count()
        {
            var postvalues = new Dictionary<string, string>()
            {
                { "t", tablename }
            };
            string s = RequestHelper.sendRequest(Counturl, JsonConvert.SerializeObject(postvalues, jsondefaultsettings)).Replace(Environment.NewLine, "");

            int result = 0;
            int.TryParse(s, out result);
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
