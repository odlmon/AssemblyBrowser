using System;
using AssemblyReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssemblyReaderTester
{
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
            Assert.AreEqual("AssemblyReaderTester", assemblyInfo.Namespaces[1].Name);
            Assert.AreEqual(1, assemblyInfo.Namespaces[1].Classes.Count);
            Assert.AreEqual("AssemblyReaderTester.UnitTest", assemblyInfo.Namespaces[1].Classes[0].Name);
            Assert.AreEqual("System.String Str", assemblyInfo.Namespaces[1].Classes[0].Members[1].Values[0]);
        }
    }
}