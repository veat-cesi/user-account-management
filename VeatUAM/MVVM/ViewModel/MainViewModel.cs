using System.Windows.Media;
using VeatUAM.Core;

namespace VeatUAM.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }
        
        public HomeViewModel HomeVM { get; set; }
        
        public DiscoveryViewModel DiscoveryVM { get; set; }
        
        private object _currentView;
        private string _currentHead;

        public string CurrentHead
        {
            get => _currentHead;
            set {
                _currentHead = value;
                OnPropertyChanged();
            }
        }

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
            DiscoveryVM = new DiscoveryViewModel();
            CurrentView = HomeVM;
            CurrentHead = HomeVM.head;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
                CurrentHead = HomeVM.head;
            });
            
            DiscoveryViewCommand = new RelayCommand(o =>
            {
                CurrentView = DiscoveryVM;
                CurrentHead = DiscoveryVM.head;
            });
        }
    }
}