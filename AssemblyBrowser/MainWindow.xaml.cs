using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using AssemblyReader;

namespace AssemblyBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AssemblyInfo assemblyInfo = AssemblyReader.AssemblyReader.GetAssemblyInfo(
                    @"C:\Users\Dmitriy\Projects\CSharp\Test\Test\bin\Debug\netcoreapp3.1\Test.dll");
            InitializeTreeView(treeView, assemblyInfo);
        }

        private void InitializeTreeView(TreeView treeView, AssemblyInfo assemblyInfo)
        {
            textBox.Text = assemblyInfo.Name;
            var namespaces = new TreeViewItem();
            namespaces.Header = "Namespaces";
            InitializeNamespacesNode(namespaces, assemblyInfo);
            treeView.Items.Add(namespaces);
        }

        private void InitializeNamespacesNode(TreeViewItem namespaces, AssemblyInfo assemblyInfo)
        {
            assemblyInfo.Namespaces.ForEach(n =>
            {
                var namespaceItem = new TreeViewItem();
                namespaceItem.Header = n.Name;
                var classes = new TreeViewItem();
                classes.Header = "Classes";
                InitializeClassesNode(classes, n);
                namespaceItem.Items.Add(classes);
                namespaces.Items.Add(namespaceItem);
            });
        }

        private void InitializeClassesNode(TreeViewItem classes, NamespaceInfo namespaceInfo)
        {
            namespaceInfo.Classes.ForEach(c =>
            {
                var classItem = new TreeViewItem();
                classItem.Header = c.Name;
                var fields = new TreeViewItem();
                fields.Header = "Fields";
                InitializeClassMembersNode(fields, c, "Fields");
                var properties = new TreeViewItem();
                properties.Header = "Properties";
                InitializeClassMembersNode(properties, c, "Properties");
                var methods = new TreeViewItem();
                methods.Header = "Methods";
                InitializeClassMembersNode(methods, c, "Methods");
                classItem.Items.Add(fields);
                classItem.Items.Add(properties);
                classItem.Items.Add(methods);
                classes.Items.Add(classItem);
            });
        }

        private void InitializeClassMembersNode(TreeViewItem accumulator, ClassInfo classInfo, string memberType)
        {
            var value = (List<string>) classInfo.GetType().GetField(memberType).GetValue(classInfo);
            value.ForEach(f =>
            {
                var Item = new TreeViewItem();
                Item.Header = f;
                accumulator.Items.Add(Item);
            });
        }
    }
}