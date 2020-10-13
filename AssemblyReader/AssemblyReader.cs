using System.Linq;
using System.Reflection;

namespace AssemblyReader
{
    public static class AssemblyReader
    {
        private static BindingFlags BindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static |
                                                   BindingFlags.Instance;

        public static AssemblyInfo GetAssemblyInfo(string assemblyFile)
        {
            Assembly a = Assembly.LoadFrom(assemblyFile);
            AssemblyInfo assemblyInfo = new AssemblyInfo(a.GetName().Name);
            a.GetTypes().Select(t => t.Namespace).Distinct().Where(n => n != null).ToList().ForEach(n =>
            {
                NamespaceInfo namespaceInfo = new NamespaceInfo(n);
                assemblyInfo.Namespaces.Add(namespaceInfo);
                a.GetTypes().Where(t => t.IsClass && t.Namespace == n).ToList().ForEach(
                    c =>
                    {
                        ClassInfo classInfo = new ClassInfo(c.ToString());
                        namespaceInfo.Classes.Add(classInfo);
                        c.GetFields(BindingFlags).ToList().ForEach(f =>
                            classInfo.Members.First(m => m.Name == "Fields").Values.Add(f.ToString()));
                        c.GetProperties(BindingFlags).ToList().ForEach(p =>
                            classInfo.Members.First(m => m.Name == "Properties").Values.Add(p.ToString()));
                        c.GetMethods(BindingFlags).ToList().ForEach(m =>
                            classInfo.Members.First(m => m.Name == "Methods").Values.Add(m.ToString()));
                    });
            });

            return assemblyInfo;
        }
    }
}