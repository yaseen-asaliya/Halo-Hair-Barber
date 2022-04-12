using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barbar_Salon.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Barbar_Salon.ViewModels;
namespace Barbar_Salon.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditServicesPage : ContentPage
    {
        
        public EditServicesPage(MyServicesModel data)
        {
            InitializeComponent();

            EditServicesViewModel editServicesViewModel = new  EditServicesViewModel(data);
            BindingContext = editServicesViewModel;

        }


    }
}