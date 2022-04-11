using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Barbar_Salon.Models;
using Barbar_Salon.Services;
using Xamarin.Forms;
namespace Barbar_Salon.ViewModels
{
    public class MyServicesViewModel
    {
        HaloHairServices firebase;

        public ObservableCollection<MyServicesModel> Services { get; set; }

        public  MyServicesViewModel()
        {
            firebase = new HaloHairServices();
            Services = new ObservableCollection<MyServicesModel>();
            Services =  firebase.getServices();


        }

      

    }
}
