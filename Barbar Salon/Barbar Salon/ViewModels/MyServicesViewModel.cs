using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Barbar_Salon.Models;
using Barbar_Salon.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Barbar_Salon.Views;
using System.Diagnostics;

namespace Barbar_Salon.ViewModels
{
    public class MyServicesViewModel : BaseViewModel
    {
        private HaloHairServices _firebase;
        public ICommand EditButton { get; }
        public ICommand DeleteButton { get; }

        public ICommand BackButton { get; }
        public ICommand PageAddServices { get; }

        private ObservableCollection<MyServicesModel> _myServices;

        private ObservableCollection<MyServicesModel> _filltedServices;
        private static string _accessToken { get; set; }

        public ObservableCollection<MyServicesModel> MyServices
        {
            get { return _myServices; }
            set
            {
                _myServices = value;
                OnPropertyChanged();

            }
        }



        public ObservableCollection<MyServicesModel> FilltedServices
        {
            get
            {
                return _filltedServices;
            }
            set
            {
                _filltedServices = value;
                OnPropertyChanged();
            }
        }

        private async void AccessToken()
        {
            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                _accessToken = oauthToken;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public MyServicesViewModel()
        {
            AccessToken();
            _firebase = new HaloHairServices();
            FilltedServices = new ObservableCollection<MyServicesModel>();
            MyServices = new ObservableCollection<MyServicesModel>();
            MyServices = _firebase.GetServices();

            MyServices.CollectionChanged += ServicesChanged;
            EditButton = new Command(OnEditTapped);
            DeleteButton = new Command(OnDeleteTapped);

            BackButton = new Command(BackPage);
            PageAddServices = new  Command(AddServicePage);

        }

        private void ServicesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                MyServicesModel myService = e.NewItems[0] as MyServicesModel;
         
                if (myService.BarberAccessToken == _accessToken)
                {

                    FilltedServices.Add(myService);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MyServicesModel services = e.OldItems[0] as MyServicesModel;


                FilltedServices.Remove(services);

            }
        }
      
    
        private async void OnEditTapped(object obj)
        {
            MyServicesModel serviceModel = (MyServicesModel)obj;
            await Application.Current.MainPage.Navigation.PushModalAsync(new EditServicesPage(serviceModel));

        }
        private async void OnDeleteTapped(object obj)
        {
            var control = obj as MyServicesModel;
            var result = await App.Current.MainPage.DisplayAlert("Delete Services ", $"Your data are delete \nName Service {control.ServiceName}", "Yes", "Cancel");
            if (result)
            {
                await _firebase.DeleteService(control);
            }
        }


        private async void BackPage(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void AddServicePage(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddServicesPage());
        }
       


    }

}

