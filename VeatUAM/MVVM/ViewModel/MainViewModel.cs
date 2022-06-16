using System.Windows.Media;
using VeatUAM.Core;

namespace VeatUAM.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        
        public HomeViewModel HomeVM { get; set; }
        
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            } 
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CurrentView = HomeVM;
        }
    }
}