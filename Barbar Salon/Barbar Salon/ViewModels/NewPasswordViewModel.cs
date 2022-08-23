using Barbar_Salon.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Barbar_Salon.ViewModels
{
    public class NewPasswordViewModel:BaseViewModel
    {
        private string _email;
        public ICommand SendNewPassword { get; }


        public NewPasswordViewModel()
        {
            SendNewPassword = new Command(OnResetPassword);

        }
        public string Email 
        {
            get { return _email; }
            set { _email = value; }
        }


        private async void OnResetPassword()
        {
            var auth = DependencyService.Resolve<IAuth>();
            await auth.ResetPassword(_email);
            await Application.Current.MainPage.DisplayAlert("Reset Password", "please check your email ", "ok");
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());



        }
      


    }
}
