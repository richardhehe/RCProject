using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RCProject.Data
{
  public static  class Class1
    {
        public static int PowerIndex { get; set; }
        public static string comPort { get; set; }

        public static Dictionary<string, object> Object2Dictionary(this object o)
        {
            Dictionary<string, object> map = new Dictionary<string, object>();

            Type t = o.GetType();

            PropertyInfo[] pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo p in pi)
            {
                MethodInfo mi = p.GetGetMethod();

                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(o, new object[] { }));
                }
            }
            return map;
        }
    }
}
