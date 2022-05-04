using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Barbar_Salon.Models;
using Barbar_Salon.Services;
using Xamarin.Forms;

namespace Barbar_Salon.ViewModels
{
    public class EditServicesViewModel:BaseViewModel
    {
        
        public ICommand UpDateDataServices { get; }
        public ICommand BackPage { get; }

        private MyServicesModel myservices;

        HaloHairServices fireBase;

        public MyServicesModel MyServices {
            get
            {
                return myservices;
            }
            set
            {
                myservices = value;
                OnPropertyChanged();
            }
        }


        public EditServicesViewModel(MyServicesModel myServices)
        {
            MyServices = myServices;    
            fireBase = new HaloHairServices();
            UpDateDataServices = new Command(async () => await UpdateServices());
            BackPage = new Command(Back_Page);
        }

        private async Task UpdateServices()
        {

            await fireBase.UpdateService(MyServices);
            await Application.Current.MainPage.Navigation.PopModalAsync();

        }
        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }


    }
}
