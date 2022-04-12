using Barbar_Salon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barbar_Salon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyServicesPage : ContentPage
    {
        public MyServicesPage()
        {
            InitializeComponent();
        }
        public async void OnItemSelected(object sender, ItemTappedEventArgs args)
        {
            var MyServicesData = args.Item as MyServicesModel;
            if (MyServicesData != null)
            {
                await Navigation.PushModalAsync(new EditServicesPage(MyServicesData));
                MyServices.SelectedItem = null;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddServicesPage());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddOfferPage());
        }
    }
}