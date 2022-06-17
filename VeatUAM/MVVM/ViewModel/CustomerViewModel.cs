using System;
using System.Collections.Generic;
using System.Windows.Input;
using VeatUAM.MVVM.Model;

namespace VeatUAM.MVVM.ViewModel
{
    public class CustomerViewModel
    {
        public string head = "Customers";

        private IList<CustomerModel> _customersDevList;

        public CustomerViewModel()
        {
            _customersDevList = new List<CustomerModel>()
            {  
                new CustomerModel{Id = 1,FirstName="Gregory",LastName="Ployart",Email="gregory.ployart@viacesi.fr",Phone="+33622334455",Password= "123456"},
                new CustomerModel{Id = 2,FirstName="Mathieu",LastName="Musard",Email="mathieu.musard@viacesi.fr",Phone="+33666778899",Password= "123456"},
                new CustomerModel{Id = 3,FirstName="Xavier",LastName="Labarbe",Email="xavier.labarbe@viacesi.fr",Phone="+33722334455",Password= "123456"},
            };
        }

        public IList<CustomerModel> CustomersDevList
        {
            get => _customersDevList;
            set => _customersDevList = value;
        }
        
        private ICommand _mUpdater;  
        public ICommand UpdateCommand  
        {  
            get  
            {  
                if (_mUpdater == null)  
                    _mUpdater = new Updater();  
                return _mUpdater;  
            }  
            set  
            {  
                _mUpdater = value;  
            }  
        }  
  
        private class Updater : ICommand  
        {

            public bool CanExecute(object parameter)  
            {  
                return true;  
            }  
  
            public event EventHandler CanExecuteChanged;  
  
            public void Execute(object parameter)  
            {  
  
            }  
            
        }  
    }
}