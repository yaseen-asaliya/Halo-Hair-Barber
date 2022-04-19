using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Barbar_Salon.Services;

namespace Barbar_Salon.ViewModels
{
    public class SignUpViewModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string namesalon { get; set; }
        public long phone { get; set; }
        public string location { get; set; }

        public ICommand SigUpCommad { get; }

        
        IAuth auth;

        HaloHairServices firebase;
        
        public SignUpViewModel()
        {
            auth = DependencyService.Get<IAuth>();
            firebase = new HaloHairServices();
            SigUpCommad = new Command(async () => await SignUp(email, password));

        }

        private async void AddUser(string name, string namesalon, long phone, string ulr, string location)
        {
            await firebase.AddNewUser(name, namesalon, phone, ulr, location);
        }


        private async Task SignUp(string email, string password)
        {

            try
            {
                string ulr = await auth.SignUpWithEmailAndPassword(email, password);
                Console.WriteLine(ulr);

                if (null != ulr)
                {
                    AddUser(name, namesalon, phone, ulr, location);
                    await Application.Current.MainPage.DisplayAlert("Successful", "Register User", "ok");

                }
                else if (ulr == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Failed", "Register User", "ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The Exceptions : " + ex);
            }
        }

    }
}
