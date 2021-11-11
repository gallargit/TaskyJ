using System.Reflection;
using System.Resources;
using TaskyJ.Globals.Data.DataObjects;

namespace TaskyJ.Globals.Data.Helpers
{
    public class ResourceHelper
    {
        public static string GetEmbeddedResource(string resourceName)
        {
            Assembly assembly = typeof(DBCategoryJ).GetTypeInfo().Assembly;
            ResourceManager rm = new ResourceManager(assembly.GetName().Name + ".Resources.ImagesResource", assembly);
            return rm.GetString(resourceName);
        }
    }
}
