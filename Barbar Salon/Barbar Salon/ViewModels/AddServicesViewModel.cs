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
        public string Service_Name { get; set; }
        public int Time_Needed { get; set; }
        public int Prices { get; set; }
        public string Deseription { get; set; }

        HaloHairServices fireBaseHaloHair;

        public ICommand AddServicesCommand { get; }
        public ICommand BackPage { get; }

        public AddServicesViewModel()
        {
            fireBaseHaloHair = new HaloHairServices();
            AddServicesCommand = new Command(async () => await AddServices());
            BackPage = new Command(Back_Page);

        }

        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }




        Random rnd;


        private async Task AddServices()
        {
            rnd = new Random();
            int id = rnd.Next(0, 1236963000);
            if (Service_Name !=null && Time_Needed != null&& Prices !=null && Deseription!=null)
            {
                ServiceModel addServices = new ServiceModel();
                {
                    addServices.Service_Name = Service_Name;
                    addServices.Time_Needed = Time_Needed;
                    addServices.Prices = Prices;
                    addServices.Deseription = Deseription;
                    addServices.ID_Services = id;
                }

                await fireBaseHaloHair.AddService(addServices);
                await Application.Current.MainPage.DisplayAlert("Successful", "Services Added ", "Ok");
                await Application.Current.MainPage.Navigation.PopModalAsync();

            }
            else

                await Application.Current.MainPage.DisplayAlert("Failed", "please fill all field to add services ", "Ok");

        }

    }
}
