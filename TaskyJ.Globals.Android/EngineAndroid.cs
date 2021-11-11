using Autofac;
using TaskyJ.DataRepo;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Globals.Data.Helpers;

namespace TaskyJ.Globals
{
    public static class EngineAndroid
    {
        private static IContainer container = null;
        private static object lockengine = new object();

        public static void InitializeDBEngines()
        {
            //ConfigurationManager.AppSettings["STSDBHTTPBaseURL"] //Configuration manager does not work for Android
            //string url = "http://192.168.0.116:7183";
            string urlstsdb = "http://10.0.2.2:7183";
            string urlmongodb = "http://10.0.2.2:27017";

            var builder = new ContainerBuilder();
            /*TODO builder.RegisterType<TaskyJRepositoryAnd>().As<IRepositoryBase<DBTaskJ>>().Keyed<IRepositoryBase<DBTaskJ>>("sqlite").SingleInstance();
            builder.RegisterType<CategoryJRepositoryAnd>().As<IRepositoryBase<DBCategoryJ>>().Keyed<IRepositoryBase<DBCategoryJ>>("sqlite").SingleInstance();*/
            builder.RegisterType<TaskyJRepositorySTSDB>().As<IRepositoryBase<DBTaskJ>>().Keyed<IRepositoryBase<DBTaskJ>>("stsdb").WithParameter("BaseURL", urlstsdb).SingleInstance();
            builder.RegisterType<CategoryJRepositorySTSDB>().As<IRepositoryBase<DBCategoryJ>>().Keyed<IRepositoryBase<DBCategoryJ>>("stsdb").WithParameter("BaseURL", urlstsdb).SingleInstance();
            builder.RegisterType<UserJRepositorySTSDB>().As<IRepositoryBase<DBUserJ>>().Keyed<IRepositoryBase<DBUserJ>>("stsdb").WithParameter("BaseURL", urlstsdb).SingleInstance();

            builder.RegisterType<TaskyJRepositoryMongoDB>().As<IRepositoryBase<DBTaskJ>>().Keyed<IRepositoryBase<DBTaskJ>>("mongodb").WithParameter("BaseURL", urlmongodb).SingleInstance();
            builder.RegisterType<CategoryJRepositoryMongoDB>().As<IRepositoryBase<DBCategoryJ>>().Keyed<IRepositoryBase<DBCategoryJ>>("mongodb").WithParameter("BaseURL", urlmongodb).SingleInstance();
            builder.RegisterType<UserJRepositoryMongoDB>().As<IRepositoryBase<DBUserJ>>().Keyed<IRepositoryBase<DBUserJ>>("mongodb").WithParameter("BaseURL", urlmongodb).SingleInstance();

            container = builder.Build();
        }

        public static IRepositoryBase<DBTaskJ> ResolveRepo(string dbengine)
        {
            lock (lockengine)
            {
                if (container == null)
                    InitializeDBEngines();

                //insert demo data for tests
                if (dbengine == "stsdb" || dbengine == "mongodb")
                {
                    var repoUsr = container.ResolveKeyed<IRepositoryBase<DBUserJ>>(dbengine);
                    var alreadyExisting = repoUsr.GetById(1) != null;
                    if (!alreadyExisting)
                    {
                        var usr1 = new DBUserJ { ID = 1, UserName = "admin", Password = "admin" };
                        var usr2 = new DBUserJ { ID = 2, UserName = "Administrador", Password = "Administrador" };
                        var usr3 = new DBUserJ { ID = 3, UserName = "string", Password = "string" };
                        repoUsr.Add(usr1);
                        repoUsr.Add(usr2);
                        repoUsr.Add(usr3);

                        var repoCat = container.ResolveKeyed<IRepositoryBase<DBCategoryJ>>(dbengine);
                        var cat1 = new DBCategoryJ { ID = 1, Name = "reminder", IconBase64 = ResourceHelper.GetEmbeddedResource("reminder") };
                        var cat2 = new DBCategoryJ { ID = 2, Name = "home", IconBase64 = ResourceHelper.GetEmbeddedResource("home") };
                        var cat3 = new DBCategoryJ { ID = 3, Name = "work", IconBase64 = ResourceHelper.GetEmbeddedResource("work") };

                        repoCat.Add(cat1);
                        repoCat.Add(cat2);
                        repoCat.Add(cat3);

                        var repo = container.ResolveKeyed<IRepositoryBase<DBTaskJ>>(dbengine);
                        repo.Add(new DBTaskJ { ID = 1, Name = "bugfix", Description = "Clean bugs found", Completed = 75, Priority = DBTaskJ.TaskPriority.Critical, Category = cat1, User = usr1 });
                        repo.Add(new DBTaskJ { ID = 2, Name = "compile", Description = "Compile sources", Completed = 20, Priority = DBTaskJ.TaskPriority.Normal, Category = cat2, User = usr1 });
                        repo.Add(new DBTaskJ { ID = 3, Name = "finish testing", Description = "Finish usecase tests", Priority = DBTaskJ.TaskPriority.Idle, Category = cat3, User = usr1 });
                        repo.Add(new DBTaskJ { ID = 4, Name = "build release", Description = "Build release package", User = usr1 });

                        repo.Add(new DBTaskJ { ID = 5, Name = "task one str", Description = "description1", User = usr3 });
                        repo.Add(new DBTaskJ { ID = 6, Name = "task two str", Description = "description2", User = usr3 });
                        repo.Add(new DBTaskJ { ID = 7, Name = "task three str", Description = "description3", User = usr3 });
                    }
                }
                //demo data ends
            }
            return container.ResolveKeyed<IRepositoryBase<DBTaskJ>>(dbengine);
        }

        public static IRepositoryBase<DBCategoryJ> ResolveRepoCat(string dbengine)
        {
            return container.ResolveKeyed<IRepositoryBase<DBCategoryJ>>(dbengine);
        }

        public static IRepositoryBase<DBUserJ> ResolveRepoUsr(string dbengine)
        {
            return container.ResolveKeyed<IRepositoryBase<DBUserJ>>(dbengine);
        }
    }
}
