using System.Windows.Media;
using VeatUAM.Core;

namespace VeatUAM.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }
        
        public RelayCommand CustomerViewCommand { get; set; }
        
        public HomeViewModel HomeVM { get; set; }
        
        public CustomerViewModel CustomerVM { get; set; }
        
        
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
            CustomerVM = new CustomerViewModel();
            CurrentView = HomeVM;
            CurrentHead = HomeVM.head;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
                CurrentHead = HomeVM.head;
            });
            
            CustomerViewCommand = new RelayCommand(o =>
            {
                
                CurrentView = CustomerVM;
                CurrentHead = CustomerVM?.head;
            });
        }
    }
}