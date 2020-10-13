using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

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
                        c.GetMethods(BindingFlags).Where(m => !m.IsDefined(typeof(ExtensionAttribute))).ToList().ForEach(m =>
                            classInfo.Members.First(m => m.Name == "Methods").Values.Add(m.ToString()));
                    });
                a.GetTypes().Where(t => t.IsClass && t.Namespace == n).ToList().ForEach(
                    c =>
                    {
                        c.GetMethods().Where(m => m.IsDefined(typeof(ExtensionAttribute), false)).ToList().ForEach(m =>
                        {
                            ClassInfo extendedType = null;
                            assemblyInfo.Namespaces.ToList().ForEach(n =>
                                extendedType = n.Classes.First(c => c.Name == m.GetParameters()[0].ParameterType.ToString()));
                            extendedType?.Members.First(m => m.Name == "Methods").Values.Add(m.ToString());
                        });
                    });
            });

            return assemblyInfo;
        }
    }
}