using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{

    public static class IEnumerableExtensions
    {
        public static List<T> Where<T>(this IEnumerable<T> source, Expression<Func<DBTaskJ, bool>> predicate)
        {
            var lst = new List<T>();
            foreach (T element in source.Where(predicate))
                lst.Add(element);
            return lst;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }
    }

    public class TaskyJRepositoryWinSQLite : RepositoryBaseWin<DBTaskJ>
    {
        public override void Add(DBTaskJ objectToAdd)
        {
            using (DbCommand cmd = dbConn.CreateCommand())
            {
                //parameterized insert
                cmd.CommandText = @"INSERT INTO TaskyJ(ID,Name,Description,Completed,Deleted,CreationDate,FinishDate,Priority,Deadline,IDCategory) VALUES(@id,@n,@d,@cm,@dlt,@cd,@fd,@pr,@dln,@idc)";

                if (objectToAdd.ID == 0)
                {
                    //this should be done by the database
                    if (Count() == 0)
                        objectToAdd.ID = 1;
                    else
                        objectToAdd.ID = 1 + GetAll().Max(x => x.ID);
                }

                DBTaskJ dbtask = objectToAdd;

                var p1 = cmd.CreateParameter();
                p1.ParameterName = "@id";
                p1.Value = dbtask.ID;

                var p2 = cmd.CreateParameter();
                p2.ParameterName = "@n";
                p2.Value = dbtask.Name;

                var p3 = cmd.CreateParameter();
                p3.ParameterName = "@d";
                p3.Value = dbtask.Description;

                var p4 = cmd.CreateParameter();
                p4.ParameterName = "@cm";
                p4.Value = dbtask.Completed;

                var p5 = cmd.CreateParameter();
                p5.ParameterName = "@dlt";
                p5.Value = dbtask.Deleted;

                var p6 = cmd.CreateParameter();
                p6.ParameterName = "@cd";
                p6.Value = dbtask.CreationDate;

                var p7 = cmd.CreateParameter();
                p7.ParameterName = "@fd";
                p7.Value = dbtask.FinishDate;

                var p8 = cmd.CreateParameter();
                p8.ParameterName = "@pr";
                p8.Value = dbtask.Priority;

                var p9 = cmd.CreateParameter();
                p9.ParameterName = "@dln";
                p9.Value = dbtask.Deadline;


                var p10 = cmd.CreateParameter();
                p10.ParameterName = "@idc";
                p10.Value = dbtask.IDCategory;

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);
                cmd.Parameters.Add(p9);
                cmd.Parameters.Add(p10);

                cmd.ExecuteNonQuery();
            }
        }

        public override IEnumerable<DBTaskJ> Find(Expression<Func<DBTaskJ, bool>> predicate)
        {
            return GetAll().AsQueryable().Where(predicate.Compile());
        }

        public override IEnumerable<DBTaskJ> GetAll()
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            var lstResult = new List<DBTaskJ>();
            using (DbCommand cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = @"SELECT ID, Name, Description, CreationDate, FinishDate, Completed, Deleted, Priority, Deadline, IDCategory FROM TaskyJ";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var t = new DBTaskJ();
                        t.ID = reader.GetInt32(0);
                        t.Name = reader.GetString(1);
                        t.Description = reader.GetString(2);
                        t.CreationDate = reader.GetDateTime(3);
                        t.FinishDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                        t.Completed = reader.GetByte(5);
                        t.Deleted = reader.GetBoolean(6);
                        t.SetPriority(reader.GetByte(7));
                        t.Deadline = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8);
                        t.IDCategory = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9);

                        lstResult.Add(t);
                    }
                }
                cmd.Dispose();
            }
            return lstResult;
        }

        public override DBTaskJ GetById(int id)
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            using (DbCommand cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = @"SELECT ID, Name, Description, CreationDate, FinishDate, Completed, Deleted, Priority, Deadline, IDCategory FROM TaskyJ WHERE ID=@ID";
                DbParameter p1 = cmd.CreateParameter();
                p1.ParameterName = "ID";
                p1.Value = id;
                cmd.Parameters.Add(p1);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var t = new DBTaskJ();
                        t.ID = reader.GetInt32(0);
                        t.Name = reader.GetString(1);
                        t.Description = reader.GetString(2);
                        t.CreationDate = reader.GetDateTime(3);
                        t.FinishDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                        t.Completed = reader.GetByte(5);
                        t.Deleted = reader.GetBoolean(6);
                        t.SetPriority(reader.GetByte(7));
                        t.Deadline = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8);
                        t.IDCategory = reader.IsDBNull(9) ? (int?)null : reader.GetInt32(9);

                        return t;
                    }
                }
                cmd.Dispose();
            }
            return null;
        }

        public override void Remove(DBTaskJ objectToDelete)
        {
            using (DbCommand cmd = dbConn.CreateCommand())
            {
                //parameterized insert
                cmd.CommandText = @"DELETE FROM TaskyJ WHERE ID=@id";

                var p1 = cmd.CreateParameter();
                p1.ParameterName = "@id";
                p1.Value = objectToDelete.ID;

                cmd.Parameters.Add(p1);
                cmd.ExecuteNonQuery();
            }
        }

        public override void Update(DBTaskJ objectToUpdate)
        {
            Remove(objectToUpdate);
            Add(objectToUpdate);
        }
    }
}
