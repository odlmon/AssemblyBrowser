using System;
using System.Linq;
using System.Reflection;

namespace AssemblyReader
{
    public class AssemblyReader
    {
        private static BindingFlags BindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static |
                                                   BindingFlags.Instance;

        public static AssemblyInfo GetAssemblyInfo(string assemblyFile)
        {
            Assembly a = Assembly.LoadFrom(assemblyFile);
            AssemblyInfo assemblyInfo = new AssemblyInfo(a.GetName().Name);
            a.GetTypes().Select(t => t.Namespace).Distinct().ToList().ForEach(n =>
            {
                NamespaceInfo namespaceInfo = new NamespaceInfo(n);
                assemblyInfo.Namespaces.Add(namespaceInfo);
                Console.WriteLine(n);
                a.GetTypes().Where(t => t.IsClass && t.Namespace == n).ToList().ForEach(
                    c =>
                    {
                        ClassInfo classInfo = new ClassInfo(c.ToString());
                        namespaceInfo.Classes.Add(classInfo);
                        Console.WriteLine("\t" + c);
                        c.GetFields(BindingFlags).ToList().ForEach(f =>
                        {
                            classInfo.Fields.Add(f.ToString());
                            Console.WriteLine("\t\t" + f);
                        });
                        c.GetProperties(BindingFlags).ToList().ForEach(p =>
                        {
                            classInfo.Properties.Add(p.ToString());
                            Console.WriteLine("\t\t" + p);
                        });
                        c.GetMethods(BindingFlags).ToList().ForEach(m =>
                        {
                            classInfo.Methods.Add(m.ToString());
                            Console.WriteLine("\t\t" + m);
                        });
                    });
            });

            return assemblyInfo;
        }
    }
}