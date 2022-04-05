using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Barbar_Salon.Views;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Barbar_Salon.ViewModels
{
    public class LoginViewModel:BaseViewModel
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

        IAuth auth;
        public LoginViewModel()
        {
            auth = DependencyService.Get<IAuth>();
            SubmitCommand = new Command(async () =>await SignIn(email,password));
        }

         async Task SignIn(string email,string password)
        {
            string token = await auth.LoginWithEmailAndPassword(email,password);
            if(token != string.Empty)
            {
                App.Current.MainPage=new AppShell();

            }
        }










    }
}
