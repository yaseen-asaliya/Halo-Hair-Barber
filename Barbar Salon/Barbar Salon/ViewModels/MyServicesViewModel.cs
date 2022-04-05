using Barbar_Salon.Models;
using Barbar_Salon.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Barbar_Salon.ViewModels
{
    public class MyServicesViewModel
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
