using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barbar_Salon.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barbar_Salon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTimeField : ContentView
    {
        
        public AddTimeField()
        {
            InitializeComponent();
            BindingContext = new AddTimeViewModel ();
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
    }
}