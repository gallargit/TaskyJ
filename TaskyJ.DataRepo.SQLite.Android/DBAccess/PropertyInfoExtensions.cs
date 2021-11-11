using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TaskyJ.DataObjects
{
    public static class PropertyInfoExtensions
    {
        public static PropertyInfo[] RemoveUnneededProperties(this PropertyInfo[] props, string idName)
        {
            //remove MVC columns
            var excludedColumns = new List<string>();
            excludedColumns.Add("IsValid");
            excludedColumns.Add("ID");
            excludedColumns.Add("ValidationErrors");
            if (!string.IsNullOrEmpty(idName))
                excludedColumns.Add(idName);

            //Remove no DataMembers
            foreach (var prop in props)
            {
                bool excludeProp = true;
                foreach (var attr in prop.GetCustomAttributes())
                {
                    if (attr.GetType().FullName == "System.Runtime.Serialization.DataMemberAttribute")
                        excludeProp = false;
                }
                if (excludeProp)
                    excludedColumns.Add(prop.Name);
            }

            //remove Lists
            foreach (var listcol in props.Where(p => p.PropertyType.IsConstructedGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(List<>).GetGenericTypeDefinition()))
            {
                excludedColumns.Add(listcol.Name);
            }

            return props.Where(s => !excludedColumns.Distinct().Contains(s.Name)).ToArray();
        }
    }
}
