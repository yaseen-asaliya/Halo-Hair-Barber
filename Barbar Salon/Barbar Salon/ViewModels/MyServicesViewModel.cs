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
        HaloHairServices firebase;


        private ObservableCollection<MyServicesModel> myservices;

        public ObservableCollection<MyServicesModel> MyServices
        {
            get { return myservices; }
            set
            {
                myservices = value;
                OnPropertyChanged();

            }
        }



        private ObservableCollection<MyServicesModel> filltedServices;
        public ObservableCollection<MyServicesModel> FilltedServices
        {
            get
            {
                return filltedServices;
            }
            set
            {
                filltedServices = value;
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

        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public ICommand BackPage { get; }
        public ICommand PageAddServices { get; }
        public MyServicesViewModel()
        {
            AccessToken();
            firebase = new HaloHairServices();
            FilltedServices = new ObservableCollection<MyServicesModel>();
            MyServices = new ObservableCollection<MyServicesModel>();
            MyServices = firebase.getServices();

            MyServices.CollectionChanged += serviceschanged;
            EditCommand = new Command(onEditTapped);
            DeleteCommand = new Command(onDeleteTapped);

            BackPage = new Command(Back_Page);
            PageAddServices = new  Command(addServicePage);

        }

        private void serviceschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                MyServicesModel services = e.NewItems[0] as MyServicesModel;
         
                if (services.AccessToken_Barbar == accessToken)
                {

                    FilltedServices.Add(services);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                MyServicesModel services = e.OldItems[0] as MyServicesModel;


                FilltedServices.Remove(services);

            }
        }
      
    
        private async void onEditTapped(object obj)
        {
            MyServicesModel serviceModel = (MyServicesModel)obj;
            await Application.Current.MainPage.Navigation.PushModalAsync(new EditServicesPage(serviceModel));

        }
        private async void onDeleteTapped(object obj)
        {
            var control = obj as MyServicesModel;
            var res = await App.Current.MainPage.DisplayAlert("Delete Services ", $"Your data are delete \n Name Service {control.Service_Name}", "Yes", "Cancel");
            if (res)
            {
                await firebase.DeleteService(control);
            }
        }


        private async void Back_Page(object obj)
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void addServicePage(object obj)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new AddServicesPage());
        }
       


    }

}

