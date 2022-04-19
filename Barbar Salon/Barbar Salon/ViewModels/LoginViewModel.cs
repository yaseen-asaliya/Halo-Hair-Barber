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
    public class LoginViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }
        public Command SubmitCommand { get; }
        public ICommand ResetPasswordCommad { get; }

        private async void OnForgetPassword()
        {
            // await Xamarin.Forms.Shell.Current.GoToAsync("//NewPasswordPage");
            await Application.Current.MainPage.Navigation.PushModalAsync(new NewPasswordPage());


        }


        IAuth auth;
        public LoginViewModel()
        {
            auth = DependencyService.Get<IAuth>();
            SubmitCommand = new Command(async () => await SignIn(email, password));
            ResetPasswordCommad = new Command(OnForgetPassword);
        }

        async Task SignIn(string email, string password)
        {

            if (email != null && password != null)
            {

                string token = await auth.LoginWithEmailAndPassword(email, password);
                try
                {
                    if (token != string.Empty)
                    {

                        try
                        {
                            await SecureStorage.SetAsync("oauth_token", token);
                            App.Current.MainPage = new AppShell();

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }


                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", ex.Message, "ok");
                }
            }
            else
                await Application.Current.MainPage.DisplayAlert("Failed", "Password and Email is Empty", "ok");

        }

    }
}
