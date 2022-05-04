using Barbar_Salon.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Barbar_Salon
{
    
    public partial class App : Application
    {
        IAuth auth;
        public App()
        {
            InitializeComponent();
            auth = DependencyService.Get<IAuth>();

            if (auth.IsSigIn())
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }



        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
