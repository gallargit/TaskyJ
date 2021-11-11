using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskyJ.DataRepo;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Services
{
    public static class TaskyJService
    {
        public static IRepositoryBase<DBTaskJ> repoTask = null;
        public static IRepositoryBase<DBCategoryJ> repoCat = null;
        public static IRepositoryBase<DBUserJ> repoUsr = null;
        //caches
        private static ConcurrentDictionary<int, DBCategoryJ> cacheCategory = new ConcurrentDictionary<int, DBCategoryJ>();
        private static ConcurrentDictionary<int, DBUserJ> cacheUser = new ConcurrentDictionary<int, DBUserJ>();

        public static void loadCacheCategory()
        {
            cacheCategory.Clear();
            repoCat.GetAll().ToList().ForEach(cat => cacheCategory.TryAdd(cat.ID, cat));
        }

        public static void loadCacheUser()
        {
            cacheUser.Clear();
            repoUsr.GetAll().ToList().ForEach(usr => cacheUser.TryAdd(usr.ID, usr));
        }

        public static IEnumerable<DBTaskJ> GetAllTasks(int? userID)
        {
            return repoTask.Find(t => t.Deleted == false && (!userID.HasValue || t.IDUser == userID));
        }

        public static IEnumerable<DBTaskJ> GetAllDeletedTasks(int? userID)
        {
            return repoTask.Find(t => t.Deleted == true && (!userID.HasValue || t.IDUser == userID));
        }

        public static DBCategoryJ GetCategoryById(int id)
        {
            if (cacheCategory.Count == 0)
                loadCacheCategory();
            if (!cacheCategory.ContainsKey(id))
                return null;
            return cacheCategory[id];
        }

        public static DBUserJ GetUserById(int id)
        {
            if (cacheUser.Count == 0)
                loadCacheUser();
            return cacheUser[id];
        }

        public static IEnumerable<DBCategoryJ> GetAllCategories()
        {
            if (cacheCategory.Count == 0)
                loadCacheCategory();
            return cacheCategory.Values;
        }

        public static IEnumerable<DBUserJ> GetAllUsers()
        {
            if (cacheUser.Count == 0)
                loadCacheUser();
            return cacheUser.Values;
        }

        public static DBTaskJ GetById(int id)
        {
            return repoTask.GetById(id);
        }

        public static void Update(DBTaskJ t)
        {
            if (t.ID == 0)
                repoTask.Add(t);
            else
                repoTask.Update(t);
        }

        public static void Update(DBCategoryJ c)
        {
            cacheCategory.Clear();
            if (c.ID == 0)
                repoCat.Add(c);
            else
                repoCat.Update(c);
        }

        public static void Update(DBUserJ c)
        {
            cacheUser.Clear();
            if (c.ID == 0)
                repoUsr.Add(c);
            else
                repoUsr.Update(c);
        }

        public static bool SetDeleted(DBTaskJ t)
        {
            t.Deleted = true;
            repoTask.Update(t);
            return true;
        }

        public static bool SetUndeleted(DBTaskJ t)
        {
            t.Deleted = false;
            repoTask.Update(t);
            return true;
        }

        public static bool Remove(DBTaskJ t)
        {
            repoTask.Remove(t);
            return true;
        }

        public static bool Remove(DBCategoryJ c)
        {
            cacheCategory.Clear();
            repoCat.Remove(c);
            return true;
        }

        public static bool Remove(DBUserJ c)
        {
            cacheUser.Clear();
            repoUsr.Remove(c);
            return true;
        }

        public static void ClearAllCaches()
        {
            cacheCategory.Clear();
            cacheUser.Clear();
        }
    }
}
