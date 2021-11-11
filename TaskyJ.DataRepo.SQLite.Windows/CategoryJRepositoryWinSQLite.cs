using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{
    public class CategoryJRepositoryWinSQLite : RepositoryBaseWin<DBCategoryJ>
    {
        public override void Add(DBCategoryJ objectToAdd)
        {
            using (DbCommand cmd = dbConn.CreateCommand())
            {
                //parameterized insert
                cmd.CommandText = @"INSERT INTO CategoryJ(ID,Name,IconBase64) VALUES(@id,@n,@ib64)";

                if (objectToAdd.ID == 0)
                {
                    //this should be done by the database
                    if (Count() == 0)
                        objectToAdd.ID = 1;
                    else
                        objectToAdd.ID = 1 + GetAll().Max(x => x.ID);
                }

                var dbcat = (DBCategoryJ)objectToAdd;

                var p1 = cmd.CreateParameter();
                p1.ParameterName = "@id";
                p1.Value = dbcat.ID;

                var p2 = cmd.CreateParameter();
                p2.ParameterName = "@n";
                p2.Value = dbcat.Name;

                var p3 = cmd.CreateParameter();
                p3.ParameterName = "ib64";
                p3.Value = dbcat.IconBase64;

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);

                cmd.ExecuteNonQuery();
            }
        }

        public override IEnumerable<DBCategoryJ> Find(Expression<Func<DBCategoryJ, bool>> predicate)
        {
            return GetAll().AsQueryable().Where(predicate.Compile());
        }

        public override IEnumerable<DBCategoryJ> GetAll()
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            var lstResult = new List<DBCategoryJ>();
            using (DbCommand cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = @"SELECT ID, Name, IconBase64 FROM CategoryJ";
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var t = new DBCategoryJ();
                        t.ID = reader.GetInt32(0);
                        t.Name = reader.GetString(1);
                        t.IconBase64 = reader.GetString(2);

                        lstResult.Add(t);
                    }
                }
                cmd.Dispose();
            }
            return lstResult;
        }

        public override DBCategoryJ GetById(int id)
        {
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            var lstResult = new List<BaseEntity>();
            using (DbCommand cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = @"SELECT ID, Name, IconBase64 FROM CategoryJ WHERE ID=@ID";
                DbParameter p1 = cmd.CreateParameter();
                p1.ParameterName = "ID";
                p1.Value = id;
                cmd.Parameters.Add(p1);
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var t = new DBCategoryJ();
                        t.ID = reader.GetInt32(0);
                        t.Name = reader.GetString(1);
                        t.IconBase64 = reader.GetString(2);

                        return t;
                    }
                }
                cmd.Dispose();
            }
            return null;
        }

        public override void Remove(DBCategoryJ objectToDelete)
        {
            using (DbCommand cmd = dbConn.CreateCommand())
            {
                //parameterized insert
                cmd.CommandText = @"DELETE FROM CategoryJ WHERE ID=@id";

                var p1 = cmd.CreateParameter();
                p1.ParameterName = "@id";
                p1.Value = objectToDelete.ID;

                cmd.Parameters.Add(p1);
                cmd.ExecuteNonQuery();
            }
        }

        public override void Update(DBCategoryJ objectToUpdate)
        {
            Remove(objectToUpdate);
            Add(objectToUpdate);
        }
    }
}
