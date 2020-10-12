using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AssemblyReader;
using Microsoft.Win32;

namespace AssemblyBrowser
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Model _model;

        public string AssemblyFile
        {
            get => _model.AssemblyFile;
            set
            {
                _model.AssemblyFile = value;
                AssemblyInfo = new ObservableCollection<AssemblyInfo>
                {
                    AssemblyReader.AssemblyReader.GetAssemblyInfo(_model.AssemblyFile)
                };
                OnPropertyChanged("AssemblyFile");
            }
        }

        public ObservableCollection<AssemblyInfo> AssemblyInfo
        {
            get => _model.AssemblyInfo;
            set
            {
                _model.AssemblyInfo = value;
                OnPropertyChanged("AssemblyInfo");
            }
        }

        private ICommand _openCommand;
        public ICommand OpenCommand
        {
            get
            {
                return _openCommand ??= new RelayCommand(obj =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        AssemblyFile = openFileDialog.FileName;
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel(Model model)
        {
            _model = model;
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}