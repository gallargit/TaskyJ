using System;
using System.Diagnostics;

namespace TaskyJ.MongoDBServer
{
    static class Program
    {
        private static bool mongoServerFound = false;

        public static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            var mongoServerDir = AppDomain.CurrentDomain.BaseDirectory;
            while (mongoServerDir.Length > 9 && !mongoServerFound)
            {
                var newdirr = System.IO.Path.Combine(mongoServerDir, "mongoDBbin");
                if (System.IO.Directory.Exists(newdirr))
                {
                    mongoServerDir = newdirr;
                    mongoServerFound = true;
                }
                else
                {
                    mongoServerDir = System.IO.Path.GetFullPath(System.IO.Path.Combine(mongoServerDir, ".."));
                }
            }
            if (mongoServerFound)
            {
                System.IO.Directory.SetCurrentDirectory(mongoServerDir);
                var process = Process.Start("mongod.exe", "-dbpath . --nojournal");
                process.WaitForExit();
            }
            else
            {
                throw new Exception("MongoDB not found");
            }
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            /*does not work as expected
            if (mongoServerFound)
            {
                Process.Start("taskkill.exe", "/f /im mongod.exe");
            }*/
        }
    }
}
