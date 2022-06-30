using System.Windows.Media;
using VeatUAM.Core;

namespace VeatUAM.MVVM.ViewModels
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand CustomerViewCommand { get; set; }
        
        public RelayCommand DeliveryViewCommand { get; set; }
        
        public RelayCommand DeveloperViewCommand { get; set; }
        
        public RelayCommand RestaurantViewCommand { get; set; }
        
        public RelayCommand SalesUserViewCommand { get; set; }
        
        public RelayCommand TechUserViewCommand { get; set; }
        
        public HomeViewModel HomeVM { get; set; }
        
        public CustomerViewModel CustomerVM { get; set; }
        
        public DeliveryViewModel DeliveryVM { get; set; }
        
        public DeveloperViewModel DeveloperVM { get; set; }
        
        public RestaurantViewModel RestaurantVM { get; set; }
        
        public SalesUserViewModel SalesUserVM { get; set; }
        
        public TechUserViewModel TechUserVM { get; set; }
        
        
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
            DeveloperVM = new DeveloperViewModel();
            RestaurantVM = new RestaurantViewModel();
            SalesUserVM = new SalesUserViewModel();
            TechUserVM = new TechUserViewModel();
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
            DeveloperViewCommand = new RelayCommand(o =>
            {
                
                CurrentView = DeveloperVM;
                CurrentHead = DeveloperVM?.head;
            });
            RestaurantViewCommand = new RelayCommand(o =>
            {
                
                CurrentView = RestaurantVM;
                CurrentHead = RestaurantVM?.head;
            });
            SalesUserViewCommand = new RelayCommand(o =>
            {
                
                CurrentView = SalesUserVM;
                CurrentHead = SalesUserVM?.head;
            });
            TechUserViewCommand = new RelayCommand(o =>
            {
                
                CurrentView = TechUserVM;
                CurrentHead = TechUserVM?.head;
            });
        }
    }
}