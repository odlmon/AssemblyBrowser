using System;
using AssemblyReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssemblyReaderTester
{
    [TestClass]
    public static class ExtensionClass
    {
        public static int ExtensionMethod(this UnitTest unitTest, int a, int b)
        {
            return a + b;
        }
    }
    
    [TestClass]
    public class UnitTest
    {
        private int x;
        
        public string Str { get; set; }

        public void Method()
        {
            Console.WriteLine("Method");
        }
        
        [TestMethod]
        public void TestMethod()
        {
            AssemblyInfo assemblyInfo = AssemblyReader.AssemblyReader.GetAssemblyInfo(@"..\netcoreapp3.1\AssemblyReaderTester.dll");
            Assert.AreEqual("AssemblyReaderTester", assemblyInfo.Name);
            Assert.AreEqual(1, assemblyInfo.Namespaces.Count);
            Assert.AreEqual("AssemblyReaderTester", assemblyInfo.Namespaces[0].Name);
            Assert.AreEqual(2, assemblyInfo.Namespaces[0].Classes.Count);
            Assert.AreEqual("AssemblyReaderTester.UnitTest", assemblyInfo.Namespaces[0].Classes[1].Name);
            Assert.AreEqual(3, assemblyInfo.Namespaces[0].Classes[1].Members.Count);
            Assert.AreEqual("Int32 x", assemblyInfo.Namespaces[0].Classes[1].Members[0].Values[0]);
            Assert.AreEqual("System.String Str", assemblyInfo.Namespaces[0].Classes[1].Members[1].Values[0]);
            Assert.AreEqual("Void Method()", assemblyInfo.Namespaces[0].Classes[1].Members[2].Values[2]);
            Assert.AreEqual("Int32 ExtensionMethod(AssemblyReaderTester.UnitTest, Int32, Int32)", 
                assemblyInfo.Namespaces[0].Classes[1].Members[2].Values[10]);
        }
    }
}