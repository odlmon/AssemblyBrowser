using System.Collections.Generic;

namespace AssemblyReader
{
    public class AssemblyInfo
    {
        public string Name { get; }
        public List<NamespaceInfo> Namespaces = new List<NamespaceInfo>();

        public AssemblyInfo(string name)
        {
            Name = name;
        }
    }

    public class NamespaceInfo
    {
        public string Name { get; }
        public List<ClassInfo> Classes = new List<ClassInfo>();

        public NamespaceInfo(string name)
        {
            Name = name;
        }
    }

    public class ClassInfo
    {
        public string Name { get; }
        public List<string> Fields = new List<string>();
        public List<string> Properties = new List<string>();
        public List<string> Methods = new List<string>();

        public ClassInfo(string name)
        {
            Name = name;
        }
    }
}