using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            var list = new ObservableCollection<AssemblyInfo> {assemblyInfo};
            treeView.ItemsSource = list;
        }
    }
}