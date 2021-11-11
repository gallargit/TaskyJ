using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using TaskyJ.DataRepo;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataObjects
{
    public static class DapperExtensions
    {
        public static T Insert<T>(this IDbConnection cnn, string tableName, string idName, dynamic param)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, idName, param), param);
            if (result.Count() > 0)
                return result.FirstOrDefault();
            else
            {
                //to-do not well supported for SQLite - redo
                if (typeof(T) == typeof(int))
                {
                    using (var Command = cnn.CreateCommand())
                    {
                        Command.CommandText = "select last_insert_rowid()";
                        long l = (long)Command.ExecuteScalar();
                        Int32 i = Convert.ToInt32(l);
                        return (T)Convert.ChangeType(i, typeof(T));
                    }
                }
            }
            return default(T);
        }

        public static void Update(this IDbConnection cnn, string tableName, string idName, dynamic param)
        {
            SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, idName, param), param);
        }
    }

    public abstract class RepositoryBaseDapperAnd<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly string _tableName;
        protected readonly string _idName;

        private IDbConnection cx = null;

        internal IDbConnection Connection
        {
            get
            {
                if (cx == null)
                    cx = SQLiteDBFactoryAnd.GetConnection();
                return cx;
            }
        }

        public object ConfigurationManager { get; private set; }

        public RepositoryBaseDapperAnd(string tableName, string idName)
        {
            _tableName = tableName;
            _idName = idName;
        }

        internal virtual dynamic Mapping(T item)
        {
            return item;
        }

        public virtual void Add(T item)
        {
            var props = item.GetType().GetProperties().RemoveUnneededProperties(_idName);
            var param = Expression.Parameter(item.GetType(), "p");
            Expression body = null;
            foreach (var prop in props)
            {
                if (prop.GetValue(item) != null)
                {
                    if (prop.GetValue(item).ToString() != "")
                    {
                        if (body == null)
                        {
                            body = Expression.Equal(Expression.PropertyOrField(param, prop.Name), Expression.Constant(prop.GetValue(item)));
                        }
                        else
                        {
                            if (prop.PropertyType == typeof(DateTime?))
                            {
                                /*DateTime? d = (DateTime?)prop.GetValue(item);
                                body = Expression.AndAlso(body,
                                Expression.Equal(
                                Expression.PropertyOrField(param, prop.Name),
                                //Expression.Constant(prop.GetValue(item))
                                Expression.Convert(Expression.Constant(d), typeof(DateTime?))
                                ));*/
                            }
                            else
                            {
                                body = Expression.AndAlso(body,
                                Expression.Equal(
                                Expression.PropertyOrField(param, prop.Name),
                                //Expression.Constant(prop.GetValue(item))
                                Expression.Constant(prop.GetValue(item))
                                ));
                            }
                        }
                    }
                }
            }

            if (body != null && item.ID != 0)
            {
                //to-do not well supported for SQLite - redo
                var lambda = Expression.Lambda<Func<T, bool>>(body, param);
                var resul = Find(lambda);
                if (resul != null)
                {
                    if (resul.FirstOrDefault() != null)
                    {
                        item.ID = resul.FirstOrDefault().ID;
                        return;
                    }
                }
            }

            var parameters = (object)Mapping(item);
            item.ID = Connection.Insert<int>(_tableName, _idName, parameters);
        }

        public virtual void Update(T item)
        {
            var parameters = (object)Mapping(item);
            Connection.Update(_tableName, _idName, parameters);
        }

        public virtual void Remove(T item)
        {
            Connection.Execute("DELETE FROM " + _tableName + " WHERE " + _idName + "=@ID", new { item.ID });
        }

        public virtual T GetById(int id)
        {
            if (id <= 0)
                return null;

            T item = default(T);

            item = (T)Connection.Query(GetItemInstance().GetType(), "SELECT * FROM " + _tableName + " WHERE " + _idName + "=@ID", new { ID = id }).SingleOrDefault();

            return item;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);
            IEnumerable<object> items = Connection.Query(GetItemInstance().GetType(), result.Sql, (object)result.Param);
            var rett = new List<T>();
            items.ToList().ForEach(item => rett.Add((T)item));
            return rett.AsEnumerable();
        }

        public virtual IEnumerable<T> GetAll()
        {
            IEnumerable<object> items = Connection.Query(GetItemInstance().GetType(), "SELECT * FROM " + _tableName);
            var rett = new List<T>();
            items.ToList().ForEach(item => rett.Add((T)item));
            return rett.AsEnumerable();
        }

        public int Count()
        {
            return GetAll().Count();
        }

        public void Dispose()
        {
        }

        public virtual T GetItemInstance()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
