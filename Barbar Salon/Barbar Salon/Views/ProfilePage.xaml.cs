using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barbar_Salon.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barbar_Salon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddTimePage());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MyServicesPage());
        }

        private void aboutbutton_Clicked(object sender, EventArgs e)
        {
            BoxAbout.IsVisible = true;
            aboutview.IsVisible = true;
            Settingsview.IsVisible = false;
            BoxSettings.IsVisible = false;

        }

        private void settingsbutton_Clicked(object sender, EventArgs e)
        {
            BoxSettings.IsVisible = true;
            Settingsview.IsVisible = true;
            aboutview.IsVisible = false;
            BoxAbout.IsVisible = false;
        }
    }
}