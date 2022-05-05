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
        public ICommand BackButton { get; }

        private MyServicesModel _myServices;

        private HaloHairServices _firebase;

        public MyServicesModel MyServices {
            get
            {
                return _myServices;
            }
            set
            {
                _myServices = value;
                OnPropertyChanged();
            }
        }


        public EditServicesViewModel(MyServicesModel myServices)
        {
            MyServices = myServices;
            _firebase = new HaloHairServices();
            UpDateDataServices = new Command(async () => await UpdateServices());
            BackButton = new Command(BackPage);
        }

        private async Task UpdateServices()
        {

            await _firebase.UpdateService(MyServices);
            await Application.Current.MainPage.Navigation.PopModalAsync();

        }
        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }


    }
}
