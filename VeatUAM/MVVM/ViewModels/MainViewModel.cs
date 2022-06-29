using System.Windows.Media;
using VeatUAM.Core;

namespace VeatUAM.MVVM.ViewModels
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand CustomerViewCommand { get; set; }
        
        public RelayCommand DeliveryViewCommand { get; set; }
        
        public HomeViewModel HomeVM { get; set; }
        
        public CustomerViewModel CustomerVM { get; set; }
        
        public DeliveryViewModel DeliveryVM { get; set; }
        
        
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
            DeliveryVM = new DeliveryViewModel();
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
                CurrentHead = CustomerVM.head;
            });
            
            DeliveryViewCommand = new RelayCommand(o =>
            {
                
                CurrentView = DeliveryVM;
                CurrentHead = DeliveryVM?.head;
            });
        }
    }
}