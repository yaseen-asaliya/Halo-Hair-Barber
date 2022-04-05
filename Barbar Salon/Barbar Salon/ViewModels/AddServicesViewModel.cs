using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Barbar_Salon.Services;
using Barbar_Salon.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;

namespace Barbar_Salon.ViewModels
{
    public class AddServicesViewModel : BaseViewModel
    {
        public string Service_Name { get; set; }
        public int Time_Needed { get; set; }
        public int Prices { get; set; } 
        public string Deseription { get; set; }
         



        FireBaseHaloHair fireBaseHaloHair;
       
        public ICommand AddServicesCommand { get; }
        public AddServicesViewModel()
        {
            fireBaseHaloHair = new FireBaseHaloHair();
            AddServicesCommand = new Command(async () => await AddServices(Service_Name,Time_Needed, Prices, Deseription));


        }

     


        public async Task AddServices(string Service_Name,int Time_Neede,int Prices,string Deseription)
        {
            await fireBaseHaloHair.AddService(Service_Name, Time_Neede, Prices, Deseription);
        }





    }
}
