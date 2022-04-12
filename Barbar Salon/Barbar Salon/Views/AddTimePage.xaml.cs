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
    public partial class AddTimePage : ContentPage
    {
        public AddTimePage()
        {
            InitializeComponent();
         

        }
        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
                OffTime.IsVisible = true;
            }
            else
            {
                OffTime.IsVisible = false;
            }
        }
        private void amOs_Clicked(object sender, EventArgs e)
        {
            pmOs.BackgroundColor = Color.White;
            amOs.BackgroundColor = Color.Gray;
        }

        private void pmOs_Clicked(object sender, EventArgs e)
        {
           amOs.BackgroundColor = Color.White;
            pmOs.BackgroundColor = Color.Gray;
        }

        private void amOe_Clicked(object sender, EventArgs e)
        {
            pmOe.BackgroundColor = Color.White;
            amOe.BackgroundColor = Color.Gray;
        }

        private void pmOe_Clicked(object sender, EventArgs e)
        {
            amOe.BackgroundColor = Color.White;
            pmOe.BackgroundColor = Color.Gray;
        }
    }
}