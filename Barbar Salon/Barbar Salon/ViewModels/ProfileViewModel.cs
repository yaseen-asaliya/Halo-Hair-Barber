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
        private bool isVisibleAbout;
        public bool IsVisibleAbout 
        {
            get
            {
                return isVisibleAbout;
            }
            set
            {
                isVisibleAbout = value;
                OnPropertyChanged();
            }
        }
        private bool isVisibleSettings;
        public bool IsVisibleSettings
        {
            get
            {
                return isVisibleSettings;
            }
            set
            {
                isVisibleSettings = value;
                OnPropertyChanged();
            }
        }

        public string Action { get; set; }
        private string language; 
        public string Language 
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
                OnPropertyChanged();
            }
        }
        public ICommand BackPage { get; }
        public ICommand TapLanguage { get; }
        public ICommand LogOut { get; }
        public ICommand Aboutbutton { get; }
        public ICommand Settingsbutton { get; }
        public ICommand AddTime { get; }
        public ICommand MyTime { get; }
        public ICommand MyService { get; }
        public ICommand AddOffer { get; }
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
            TapLanguage = new Command(Tap_Language);
            Aboutbutton = new Command(About_button);
            Settingsbutton = new Command(Settings_button);
            AddTime = new Command(Add_Time);
            MyService = new Command(My_Service);
            AddOffer = new Command(Add_Offer);
            MyTime = new Command(My_Time);
            Language = "English";
            IsVisibleAbout = true;
            IsVisibleSettings = false;

        }

        private async void My_Time(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MyTimePage());
        }

        private async void Add_Offer(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddOfferPage());
        }
        
        private async void My_Service(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MyServicesPage());
        }

        private async void Add_Time(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddTimePage());
        }

        private void Settings_button(object obj)
        {
            IsVisibleAbout = false;
            IsVisibleSettings = true;
        }

        private void About_button(object obj)
        {
            IsVisibleSettings = false;
            IsVisibleAbout = true;
        }

        private async void Tap_Language(object obj)
        {
             Action = await Application.Current.MainPage.DisplayActionSheet("Select Language","Cancel", null, "English", "Arabic");
            if (Action == "Cancel")
            {
                return;
            }
            Language = Action;
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