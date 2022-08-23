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
        public string Email { get; set; }
        public string Password { get; set; }
        public string SalonName { get; set; }
        public string BarberName { get; set; }
        public long Phone { get; set; }
        public string Location { get; set; }
        public string BarberAccessToken { get; set; }
        public string ConfirmPassword { get; set; }
        public ICommand SigUpButton { get; }

        private IAuth _auth;

        private HaloHairServices _firebase;
        
        public SignUpViewModel()
        {
            _auth = DependencyService.Get<IAuth>();
            _firebase = new HaloHairServices();
            SigUpButton = new Command(async () => await SignUp(Email, Password));


        }
       
        private async void AddUser(string accesstoken)
        {
            AuthenticationModel addUser = new AuthenticationModel();
            {
                addUser.BarberName = BarberName;
                addUser.SalonName = SalonName;
                addUser.Phone = Phone;
                addUser.Location = Location ;
                addUser.BarberAccessToken = accesstoken;

            }
            await _firebase.AddNewUser(addUser);
        }

        private async Task SignUp(string email, string password)
        {

            if (password == ConfirmPassword)
            {
                string accesstoken = await _auth.SignUpWithEmailAndPassword(email, password);
                    if (null != accesstoken)
                    {
                        AddUser(accesstoken);
                        await Application.Current.MainPage.DisplayAlert("Successful", "Register User", "ok");
                        await Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                    }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Failed", "Confirm password is incorrect", "ok");
            }
        }


    


    }
}
