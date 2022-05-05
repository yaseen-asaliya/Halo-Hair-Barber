using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Barbar_Salon.Services;
using System.Windows.Input;
using Barbar_Salon.Models;

namespace Barbar_Salon.ViewModels
{
    public class AddServicesViewModel
    {
        HaloHairServices _firebaseHaloHair;
        Random _random;
        public ICommand AddServicesCommand { get; }
        public ICommand BackButton { get; }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int Price { get; set; }
        public int TimeNeed { get; set; }
        public string Deseription { get; set; }

        public AddServicesViewModel()
        {
            _firebaseHaloHair = new HaloHairServices();
            AddServicesCommand = new Command(async () => await addServices());
            BackButton = new Command(backPage);

        }


        private async Task addServices()
        {
            _random = new Random();
            int id = _random.Next(0, 1236963000);
            if (ServiceName != null && Deseription != null)
            {
                MyServicesModel _addService = new MyServicesModel();
                {
                    _addService.ServiceName = ServiceName;
                    _addService.TimeNeed = TimeNeed;
                    _addService.Price = Price;
                    _addService.Deseription = Deseription;
                    _addService.ServiceId = id;
                }
                await _firebaseHaloHair.AddService(_addService);
                await Application.Current.MainPage.DisplayAlert("Successful", "Services Added ", "Ok");
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "please fill all field to add services ", "Ok");
            }
        }
        private async void backPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }



    }
}
