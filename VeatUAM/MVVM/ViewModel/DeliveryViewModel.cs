using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using VeatUAM.MVVM.Model;

namespace VeatUAM.MVVM.ViewModel
{
    public class DeliveryViewModel
    {
        public string head = "Delivery";

        private IList<DeliveryModel> _deliveryDevList;

        public DeliveryViewModel()
        {
            _deliveryDevList = new List<DeliveryModel>()
            {  
                new DeliveryModel{Id = 1,FirstName="Léo",LastName="Delpond",Email="leo.delpond@viacesi.fr",Phone="+33622334455",Password= "123456"},
                new DeliveryModel{Id = 2,FirstName="Harry",LastName="Jomag",Email="harry.jmg@freelance.com",Phone="+33666778899",Password= "123456"},
            };
        }

        public IList<DeliveryModel> DeliveryDevList
        {
            get => _deliveryDevList;
            set => _deliveryDevList = value;
        }
    }
}