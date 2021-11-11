using Autofac;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using TaskyJ.DataRepo;
using TaskyJ.Globals.Data.DataObjects;
using TaskyJ.Globals.Data.Helpers;

namespace TaskyJ.Globals
{
    public static class EngineWindows
    {
        private static IContainer container = null;
        private static readonly object lockengine = new object();

        private static string STSDBHTTPBaseURL = "";

        public static void InitializeDBEngines()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TaskyJRepositorySTSDB>().As<IRepositoryBase<DBTaskJ>>().Keyed<IRepositoryBase<DBTaskJ>>("stsdb").WithParameter("BaseURL", STSDBHTTPBaseURL).SingleInstance();
            builder.RegisterType<CategoryJRepositorySTSDB>().As<IRepositoryBase<DBCategoryJ>>().Keyed<IRepositoryBase<DBCategoryJ>>("stsdb").WithParameter("BaseURL", STSDBHTTPBaseURL).SingleInstance();
            builder.RegisterType<UserJRepositorySTSDB>().As<IRepositoryBase<DBUserJ>>().Keyed<IRepositoryBase<DBUserJ>>("stsdb").WithParameter("BaseURL", STSDBHTTPBaseURL).SingleInstance();

            builder.RegisterType<TaskyJRepositoryMongoDB>().As<IRepositoryBase<DBTaskJ>>().Keyed<IRepositoryBase<DBTaskJ>>("mongodb").SingleInstance();
            builder.RegisterType<CategoryJRepositoryMongoDB>().As<IRepositoryBase<DBCategoryJ>>().Keyed<IRepositoryBase<DBCategoryJ>>("mongodb").SingleInstance();
            builder.RegisterType<UserJRepositoryMongoDB>().As<IRepositoryBase<DBUserJ>>().Keyed<IRepositoryBase<DBUserJ>>("mongodb").SingleInstance();

            builder.RegisterType<TaskyJRepositoryWinSqlSrv>().As<IRepositoryBase<DBTaskJ>>().Keyed<IRepositoryBase<DBTaskJ>>("sqlsrv").SingleInstance();
            builder.RegisterType<CategoryJRepositoryWinSqlSrv>().As<IRepositoryBase<DBCategoryJ>>().Keyed<IRepositoryBase<DBCategoryJ>>("sqlsrv").SingleInstance();
            //TODO pending builder.RegisterType<UserJRepositoryWinSqlSrv>().As<IRepositoryBase<DBUserJ>>().Keyed<IRepositoryBase<DBUserJ>>("sqlsrv").SingleInstance();

            builder.RegisterType<TaskyJRepositoryWinSQLite>().As<IRepositoryBase<DBTaskJ>>().Keyed<IRepositoryBase<DBTaskJ>>("sqlite").SingleInstance();
            builder.RegisterType<CategoryJRepositoryWinSQLite>().As<IRepositoryBase<DBCategoryJ>>().Keyed<IRepositoryBase<DBCategoryJ>>("sqlite").SingleInstance();
            //TODO pending builder.RegisterType<UserJRepositoryWinSQLite>().As<IRepositoryBase<DBUserJ>>().Keyed<IRepositoryBase<DBUserJ>>("sqlite").SingleInstance();

            container = builder.Build();
        }

        //TODO move dbParameters to an "init()" method
        public static IRepositoryBase<DBTaskJ> ResolveRepoTask(string dbengine, System.Collections.Generic.Dictionary<string, string> dbParameters)
        {
            /*
            var filetoconvert1 = @"%userprofile%\Downloads\icons8-reminder-50.png";
            var filetoconvert2 = @"%userprofile%\Downloads\icons8-home-50.png";
            var filetoconvert3 = @"%userprofile%\Downloads\icons8-work-50.png";
            var a = System.Drawing.Image.FromFile(filetoconvert1);
            File.WriteAllText(filetoconvert1 + ".txt", ConvertImageToBase64(filetoconvert1));
            File.WriteAllText(filetoconvert2 + ".txt", ConvertImageToBase64(filetoconvert2));
            File.WriteAllText(filetoconvert3 + ".txt", ConvertImageToBase64(filetoconvert3));
            */

            lock (lockengine)
            {
                //manual file semaphore
                var locksemaphore = false;
                var filepath = Assembly.GetExecutingAssembly().CodeBase;
                if (filepath.ToLower().Contains("windows.dll") && filepath.ToLower().IndexOf("bin/debug") > 0)
                {
                    filepath = filepath.Substring(3 + filepath.IndexOf("///"));
                    filepath = filepath.Substring(0, filepath.IndexOf("bin/Debug"));
                    filepath += "../LOCK.txt";
                    if (File.Exists(filepath))
                    {
                        if (File.GetCreationTime(filepath).AddSeconds(30) < DateTime.Now)
                        {
                            File.Delete(filepath);
                            File.WriteAllText(filepath, Assembly.GetExecutingAssembly().CodeBase);
                        }
                        else
                        {
                            locksemaphore = true;
                            //wait for initialization to finish
                            while (File.Exists(filepath) && File.GetCreationTime(filepath).AddSeconds(30) > DateTime.Now)
                            {
                                Thread.Sleep(1000);
                            }
                        }
                    }
                }

                if (container == null)
                {
                    if (dbParameters.ContainsKey("STSDBHTTPBaseURL"))
                        STSDBHTTPBaseURL = dbParameters["STSDBHTTPBaseURL"];
                    InitializeDBEngines();
                    //insert demo data for tests
                    if (dbengine == "stsdb" || dbengine == "mongodb")
                    {
                        /*
                        var profileImage = BitmapLoader.Current.LoadFromResource(@"C:\Users\IBM_ADMIN\Downloads\icons8-reminder-50.png", null, null).Result;
                        var txt = ImageHelper.ImageToBase64(profileImage);
                        var img = ImageHelper.Base64StringToImage(txt);
                        var reminderr = GetEmbeddedResource("reminder");
                        img = ImageHelper.Base64StringToImage(GetEmbeddedResource("reminder"));
                        */

                        if (!locksemaphore)
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
                            try
                            {
                                File.Delete(filepath);
                            }
                            catch { }
                        }
                        //demo data ends
                    }
                }
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
