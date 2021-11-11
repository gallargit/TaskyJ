using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.DataRepo
{

    public class CategoryJRepositoryWinSqlSrv : RepositoryBaseWin<DBCategoryJ>
    {

        public override IEnumerable<DBCategoryJ> GetAll()
        {
            return dbConn.CategoryJTable;
        }

        protected CategoryJTable getDbObject(DBCategoryJ originalObject)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DBCategoryJ, TaskyJTable>();
            });
            return config.CreateMapper().Map<DBCategoryJ, CategoryJTable>(originalObject);
        }

        public override void Add(DBCategoryJ objectToAdd)
        {
            dbConn.CategoryJTable.Add(getDbObject(objectToAdd));
            dbConn.SaveChanges();
        }

        public override DBCategoryJ GetById(int id)
        {
            var result = dbConn.CategoryJTable.Find(id);
            if (result != null)
                return result;
            return null;
        }

        public override void Remove(DBCategoryJ objectToDelete)
        {
            dbConn.CategoryJTable.Remove(dbConn.CategoryJTable.Find(objectToDelete.ID));
            dbConn.SaveChanges();
        }

        public override void Update(DBCategoryJ objectToUpdate)
        {
            dbConn.CategoryJTable.Find(objectToUpdate.ID).CopyFrom(objectToUpdate);
            dbConn.SaveChanges();
        }

        public override IEnumerable<DBCategoryJ> Find(Expression<Func<DBCategoryJ, bool>> predicate)
        {
            return dbConn.CategoryJTable.Where(predicate.Compile());
        }

    }
}
