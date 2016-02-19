using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TimeView.data;
using TimeView.wpf.Services;
using TimeView.wpf.Utility;
using TimeView.wpf.Extensions;

namespace TimeView.wpf.ViewModel
{
    public class OpenDataListViewModel : INotifyPropertyChanged
    {
        public ICommand UpdateOpenDataCommand { get; set; }

        private readonly IOpenDataDataService _openDataDataService;
        private ObservableCollection<Company> _companies;
        public ObservableCollection<Company> Companies
        {
            get { return _companies; }
            set
            {
                _companies = value;
                RaisePropertyChanged("Companies");
            }
        }

        public OpenDataListViewModel(IOpenDataDataService openDataDataService)
        {
            // set services
            _openDataDataService = openDataDataService;

            // Load data & commands
            LoadCommands();
        }

        private void LoadCommands()
        {
            UpdateOpenDataCommand = new CustomCommand(OpenOpenData, CanAlwaysOpen);
        }

        private bool CanAlwaysOpen(object obj)
        {
            return true;
        }

        private void OpenOpenData(object obj)
        {
            UpdateOpenData();
        }

        private async void UpdateOpenData()
        {
            var companies = await _openDataDataService.UpdateOpenData();
            Companies = companies.ToObservableCollection();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
