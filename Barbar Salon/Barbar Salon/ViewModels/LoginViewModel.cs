using Barbar_Salon.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Barbar_Salon.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        private IAuth _auth;
        public Command SubmitCommand { get; }
        public ICommand ResetPasswordCommad { get; }
        public ICommand SignUpPageCommad { get; }

        private event PropertyChangedEventHandler _propertyChanged = delegate { };
        private string _email;
        private string _password;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                _propertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                _propertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
      
 
        public LoginViewModel()
        {
            _auth = DependencyService.Get<IAuth>();
            SubmitCommand = new Command(async () => await SignIn(_email, _password));
            ResetPasswordCommad = new Command(OnForgetPassword);
            SignUpPageCommad = new Command(OnSignUpPage);
        }



        private async Task SignIn(string email, string password)
        {
            IsBusy = true;
            if (email != null && password != null)
            {
                string token = await _auth.LoginWithEmailAndPassword(email, password);
                if (token != string.Empty)
                {
                    try
                    {
                        await SecureStorage.SetAsync("oauth_token", token);
                        IsBusy = false;
                        App.Current.MainPage = new AppShell();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }
                IsBusy = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Email And Password is Empty", "ok");
                IsBusy = false;
            }

        }
        private async void OnForgetPassword()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new NewPasswordPage());
        }
        private async void OnSignUpPage()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new SignUpPage());
        }

    }
}
