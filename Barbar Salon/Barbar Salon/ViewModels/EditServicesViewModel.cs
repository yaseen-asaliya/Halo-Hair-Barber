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
    public class EditServicesViewModel
    {
        public string Service_Name { get; set; }
        public int Prices { get; set; }
        public int Time_Needed { get; set; }
        public string Deseription { get; set; }
        public ICommand UpDateDataServices { get; }

        HaloHairServices fireBase;

        public EditServicesViewModel()
        {
            fireBase = new HaloHairServices();

            UpDateDataServices = new Command(async () => await UpdateServices());

        }



        public async Task UpdateServices()
        {


            await fireBase.UpdateService(Service_Name, Time_Needed, Prices, Deseription);
        }



    }


}
