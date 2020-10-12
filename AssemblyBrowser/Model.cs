using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssemblyReader;

namespace AssemblyBrowser
{
    public class Model : INotifyPropertyChanged
    {
        private string _assemblyFile;
        public string AssemblyFile
        {
            get => _assemblyFile;
            set
            {
                _assemblyFile = value;
                OnPropertyChanged("AssemblyFile");
            }
        }

        private ObservableCollection<AssemblyInfo> _assemblyInfo;
        public ObservableCollection<AssemblyInfo> AssemblyInfo
        {
            get => _assemblyInfo;
            set
            {
                _assemblyInfo = value;
                OnPropertyChanged("AssemblyInfo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}