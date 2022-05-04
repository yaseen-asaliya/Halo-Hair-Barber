using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Barbar_Salon.Services;
using Barbar_Salon.Views;
using Barbar_Salon.Models;

namespace Barbar_Salon.ViewModels
{
    public class SignUpViewModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string ConfirmPassword { get; set; }
        public string name { get; set; }
        public string namesalon { get; set; }
        public long phone { get; set; }
        public string location { get; set; }

        public ICommand SigUpCommad { get; }

        public ICommand BackPage { get; }



        IAuth auth;

        HaloHairServices firebase;
        
        public SignUpViewModel()
        {
            auth = DependencyService.Get<IAuth>();
            firebase = new HaloHairServices();

            SigUpCommad = new Command(async () => await SignUp(email, password));

            BackPage = new Command(Back_Page);


        }
       
        private async void AddUser()
        {
            AuthenticationModel addUser = new AuthenticationModel();
            {
                addUser.Name = name;
                addUser.NameSalon = namesalon;
                addUser.Phone = phone;
                addUser.location = location;

            }
            await firebase.AddNewUser(addUser);
        }


        private async Task SignUp(string email, string password)
        {

            if (password == ConfirmPassword)
            {
                string ulr = await auth.SignUpWithEmailAndPassword(email, password);
                    if (null != ulr)
                    {
                        AddUser();
                        await Application.Current.MainPage.DisplayAlert("Successful", "Register User", "ok");
                        await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                    }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Confirm password is incorrect", "ok");
            }
        }


        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }


    }
}
