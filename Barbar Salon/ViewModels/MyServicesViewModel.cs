using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;

using Barbar_Salon.Models;
using System.Windows.Input;

using Barbar_Salon.Services;
using System.ComponentModel;


namespace Barbar_Salon.ViewModels
{
    public class MyServicesViewModel:BaseViewModel
    {
        FireBaseHaloHair firebase;

        public ObservableCollection<MyServicesModel> Services { get; set; }


        public MyServicesViewModel()
        {
            firebase = new FireBaseHaloHair();
            Services = new ObservableCollection<MyServicesModel>();
            Services = firebase.getServices();

        }


    }
}
