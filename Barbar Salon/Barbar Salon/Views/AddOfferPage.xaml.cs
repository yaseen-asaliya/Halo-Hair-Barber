using Firebase.Database;
using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Barbar_Salon.Services;
using Barbar_Salon.Models;
using Firebase.Database.Query;
namespace Barbar_Salon.Views

{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOfferPage : ContentPage
    {
        public AddOfferPage()
        {
            InitializeComponent();
        }
    }
}