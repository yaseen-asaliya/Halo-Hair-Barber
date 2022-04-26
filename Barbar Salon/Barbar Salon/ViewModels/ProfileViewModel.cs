using Barbar_Salon.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Barbar_Salon.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Collections.Specialized;
using Xamarin.Forms;
using Barbar_Salon.Views;
namespace Barbar_Salon.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        HaloHairServices firebase;


        private ObservableCollection<ProfilePageModel> profile;

        public ObservableCollection<ProfilePageModel> Profile
        {
            get { return profile; }
            set
            {
                profile = value;
                OnPropertyChanged();

            }
        }



        private ObservableCollection<ProfilePageModel> myprofile;
        public ObservableCollection<ProfilePageModel> Myprofile
        {
            get
            {
                return myprofile;
            }
            set
            {
                myprofile = value;
                OnPropertyChanged();
            }
        }



        private static string accessToken { get; set; }

        private async Task AccessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                accessToken = oauthToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ICommand BackPage { get; }


        public ProfileViewModel()
        {
            AccessToken();
            firebase = new HaloHairServices();
            Myprofile = new ObservableCollection<ProfilePageModel>();
            Profile = new ObservableCollection<ProfilePageModel>();
            Profile = firebase.ProfilePage();
            Profile.CollectionChanged += serviceschanged;
            LogOut = new Command(PerformLogOut);
            BackPage = new Command(Back_Page);


        }

        private void serviceschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ProfilePageModel profilePageModel = e.NewItems[0] as ProfilePageModel;
                Console.WriteLine(e.NewItems[0]);
                Console.WriteLine(e.NewItems[0].GetType());
                if (profilePageModel.AccessToken_Barbar == accessToken)
                {

                    Myprofile.Add(profilePageModel);
                    SecureStorage.SetAsync("NameSoaln", profilePageModel.NameSalon.ToString());
                    SecureStorage.SetAsync("location", profilePageModel.location.ToString());


                }
            }

        }



        public ICommand LogOut { get; }


        private void PerformLogOut()
        {
            var auth = DependencyService.Resolve<IAuth>();
            auth.IsSigOut();
            var oauthToken = SecureStorage.Remove("oauth_token");
            Xamarin.Forms.Shell.Current.GoToAsync("//LoginPage");
        }
        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }


    }
}