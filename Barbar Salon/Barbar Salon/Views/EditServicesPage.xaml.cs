using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barbar_Salon.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barbar_Salon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditServicesPage : ContentPage
    {
        
        public EditServicesPage(MyServicesModel data)
        {
            InitializeComponent();
            GetInfoSalon(data);
        }

        public void GetInfoSalon(MyServicesModel data)
        {
            Service_Name.Text = data.Service_Name;
            Prices.Text=data.Prices.ToString();
            Time.Text = data.Time_Needed.ToString();



        }
    }
}