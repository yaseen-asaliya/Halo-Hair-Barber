using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Barbar_Salon.Services;
using System.Windows.Input;


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
        public AddServicesViewModel()
        {
            fireBaseHaloHair = new HaloHairServices();
            AddServicesCommand = new Command(async () => await AddServices(Service_Name, Time_Needed, Prices, Deseription));


        }


        private async Task AddServices(string Service_Name, int Time_Neede, int Prices, string Deseription)
        {
            if(Service_Name !=null && Time_Neede !=null&& Prices !=null && Deseription!=null)
            {
                await fireBaseHaloHair.AddService(Service_Name, Time_Neede, Prices, Deseription);
                await Application.Current.MainPage.DisplayAlert("Successful", "Services Added ", "Ok");


            }
            else

                await Application.Current.MainPage.DisplayAlert("Failed", "please fill all field to and services ", "Ok");

        }

    }
}
