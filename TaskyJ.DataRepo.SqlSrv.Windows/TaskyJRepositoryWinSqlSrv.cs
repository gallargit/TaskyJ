using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{

    public class TaskyJRepositoryWinSqlSrv : RepositoryBaseWin<DBTaskJ>
    {

        public override IEnumerable<DBTaskJ> GetAll()
        {
            return dbConn.TaskyJTable;
        }

        protected TaskyJTable getDbObject(DBTaskJ originalObject)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DBTaskJ, TaskyJTable>();
            });
            return config.CreateMapper().Map<DBTaskJ, TaskyJTable>(originalObject);
        }

        public override void Add(DBTaskJ objectToAdd)
        {
            dbConn.TaskyJTable.Add(getDbObject(objectToAdd));
            dbConn.SaveChanges();
        }

        public override DBTaskJ GetById(int id)
        {
            var result = dbConn.TaskyJTable.Find(id);
            if (result != null)
                return result;
            return null;
        }

        public override void Remove(DBTaskJ objectToDelete)
        {
            dbConn.TaskyJTable.Remove(dbConn.TaskyJTable.Find(objectToDelete.ID));
            dbConn.SaveChanges();
        }

        public override void Update(DBTaskJ objectToUpdate)
        {
            dbConn.TaskyJTable.Find(objectToUpdate.ID).CopyFrom(objectToUpdate);
            dbConn.SaveChanges();
        }

        public override IEnumerable<DBTaskJ> Find(Expression<Func<DBTaskJ, bool>> predicate)
        {
            return dbConn.TaskyJTable.Where(predicate.Compile());
        }

    }
}
