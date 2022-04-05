using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barbar_Salon.ViewModels;
using Barbar_Salon.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barbar_Salon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditServicesPage : ContentPage
    {
        public EditServicesPage(MyServicesModel MyServicesData)
        {
            InitializeComponent();
            DataServices(MyServicesData);
        }


        public void DataServices(MyServicesModel MyServicesData)
        {
            Prices.Text = MyServicesData.Prices.ToString();
            Time.Text = MyServicesData.Time_Needed.ToString();
            Service_Name.Text = MyServicesData.Service_Name.ToString();
            
        }










    }
}