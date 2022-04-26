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
        public NewPasswordViewModel()
        {
            SendNewPassword = new Command(OnResetPassword);

            BackPage = new Command(Back_Page);

        }
        public string Email 
        {
            get { return email; }
            set { email = value; }
        }
        private string email;
        public ICommand SendNewPassword { get; }

        public ICommand BackPage { get; }


        private async void OnResetPassword()
        {
            var auth = DependencyService.Resolve<IAuth>();
            auth.ResetPassword(email);


            await Application.Current.MainPage.DisplayAlert("Reset Password", "please check your email ", "ok");
            await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());



        }
        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }



    }
}
