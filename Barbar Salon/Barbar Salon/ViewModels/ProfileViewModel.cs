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
        private HaloHairServices _firebase;
        public ICommand TapLanguageButton { get; }
        public ICommand LogOut { get; }
        public ICommand Aboutbutton { get; }
        public ICommand Settingsbutton { get; }
        public ICommand AddTimeButton { get; }
        public ICommand MyTimeButton { get; }
        public ICommand MyServiceButton { get; }
        public ICommand AddOfferButton { get; }
        public ICommand EditlocationButton { get; }
        public ICommand EditPhoneButton { get; }
        public ICommand EditNameButton { get; }
        public ICommand EditNameSalonButton { get; }
        private static string _accessToken { get; set; }

        private bool _isVisibleAbout;

        private bool _isVisibleSettings;
        public string Action { get; set; }
        private string _language;


        private ObservableCollection<ProfilePageModel> _profile;
        private ObservableCollection<ProfilePageModel> _myProfile;


        public ObservableCollection<ProfilePageModel> Profile
        {
            get { return _profile; }
            set
            {
                _profile = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ProfilePageModel> MyProfile
        {
            get
            {
                return _myProfile;
            }
            set
            {
                _myProfile = value;
                OnPropertyChanged();
            }
        }
   
        private async void AccessToken()
        {
            try
            {
                _accessToken = await SecureStorage.GetAsync("oauth_token");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    
        public bool IsVisibleAbout 
        {
            get
            {
                return _isVisibleAbout;
            }
            set
            {
                _isVisibleAbout = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisibleSettings
        {
            get
            {
                return _isVisibleSettings;
            }
            set
            {
                _isVisibleSettings = value;
                OnPropertyChanged();
            }
        }

       
        public string Language 
        {
            get
            {
                return _language;
            }
            set
            {
                _language = value;
                OnPropertyChanged();
            }
        }

        public ProfileViewModel()
        {
            AccessToken();
            _firebase = new HaloHairServices();
            MyProfile = new ObservableCollection<ProfilePageModel>();
            Profile = new ObservableCollection<ProfilePageModel>();
            Profile = _firebase.ProfilePage();
            Profile.CollectionChanged += Servicechanged;
            LogOut = new Command(PerformLogOut);
            TapLanguageButton = new Command(TapLanguage);
            Aboutbutton = new Command(About_button);
            Settingsbutton = new Command(Settings_button);
            AddTimeButton = new Command(AddTime);
            MyServiceButton = new Command(MyService);
            AddOfferButton = new Command(AddOffer);
            MyTimeButton = new Command(MyTime);
            EditNameButton = new Command(OnEditNameCommand);
            EditlocationButton = new Command(OnEditLocationCommand);
            EditNameSalonButton = new Command(OnEditNameSalonCommand);
            EditPhoneButton = new Command(OnEditPhoneCommand);
            Language = "English";
            IsVisibleAbout = true;
            IsVisibleSettings = false;

        }

        private async void MyTime(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MyTimePage());
        }

        private async void AddOffer(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddOfferPage());
        }
        
        private async void MyService(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MyServicesPage());
        }

        private async void AddTime(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddTimePage());
        }

        private async void TapLanguage(object obj)
        {
            Action = await Application.Current.MainPage.DisplayActionSheet("Select Language", "Cancel", null, "English", "Arabic");
            if (Action == "Cancel")
            {
                return;
            }
            Language = Action;
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

        private void Servicechanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ProfilePageModel profilePageModel = e.NewItems[0] as ProfilePageModel;
                if (profilePageModel.BarberAccessToken == _accessToken)
                {
                    MyProfile.Remove(profilePageModel);
                    MyProfile.Add(profilePageModel);
                    SecureStorage.SetAsync("NameSoaln", profilePageModel.SalonName.ToString());
                    SecureStorage.SetAsync("location", profilePageModel.Location.ToString());
                }
            }
        }
        private void PerformLogOut()
        {
            var auth = DependencyService.Resolve<IAuth>();
            auth.IsSigOut();
             SecureStorage.Remove("oauth_token");
             SecureStorage.Remove("NameSoaln");
             SecureStorage.Remove("location");
            Xamarin.Forms.Shell.Current.GoToAsync("//LoginPage");
        }

        private async void OnEditNameCommand(object obj)
        {
            string NewName = await App.Current.MainPage.DisplayPromptAsync("Edit Name", "New Name");
            if (NewName != null)
            {
                MyProfile[0].BarberName = NewName;
                await _firebase.UpdateUser(MyProfile[0]);
            }
        }

        private async void OnEditLocationCommand(object sender)
        {
            string NewAddress = await App.Current.MainPage.DisplayPromptAsync("Edit Address", "New Address");
            if (NewAddress != null)
            {
                MyProfile[0].Location = NewAddress;
                await _firebase.UpdateUser(MyProfile[0]);
            }
        }
        private async void OnEditPhoneCommand(object sender)
        {
            string NewPhone = await App.Current.MainPage.DisplayPromptAsync("Edit Phone", "New Phone");
            if (NewPhone != null)
            {
                if (NewPhone.Length <= 10)
                {
                    MyProfile[0].Phone = NewPhone;
                    await _firebase.UpdateUser(MyProfile[0]);
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Faild", "The phone number invalid", "OK");
                }
            }

        }
        private async void OnEditNameSalonCommand(object sender)
        {
            string NewNameSalon = await App.Current.MainPage.DisplayPromptAsync("Edit Name Salon", "New Name Salon");
            if (NewNameSalon != null)
            {
                MyProfile[0].SalonName = NewNameSalon;
                await _firebase.UpdateUser(MyProfile[0]);
            }

        }
    }
}