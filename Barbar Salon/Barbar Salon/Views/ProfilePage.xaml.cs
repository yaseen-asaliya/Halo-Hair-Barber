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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }
        private void AddSerives(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AddServicesPage());
        }

        private void EditService(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MyServicesPage());
        }
    }
}