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
        /*
        public string Service_Name { get; set; }
        public int Prices { get; set; }
        public int Time_Needed { get; set; }
        public string Deseription { get; set; }
        
       

        */
        public ICommand UpDateDataServices { get; }

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

        private MyServicesModel myservices;
          
        public EditServicesViewModel(MyServicesModel myServices)
        {
            MyServices = myServices;    
            fireBase = new HaloHairServices();
            
            /*
            Service_Name = myServices.Service_Name;
            Prices = myServices.Prices;
            Time_Needed=myServices.Time_Needed;*/

            UpDateDataServices = new Command(async () => await UpdateServices());


        }

        private async Task UpdateServices()
        {

            await fireBase.UpdateService(MyServices);
        }

    }
}
