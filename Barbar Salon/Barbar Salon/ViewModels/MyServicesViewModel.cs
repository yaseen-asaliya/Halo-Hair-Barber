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


        public MyServicesViewModel()
        {
            AccessToken();
            firebase = new HaloHairServices();
            FilltedServices = new ObservableCollection<MyServicesModel>();
            MyServices = new ObservableCollection<MyServicesModel>();
            MyServices = firebase.getServices();

            MyServices.CollectionChanged += serviceschanged;


        }

        private void serviceschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                MyServicesModel services = e.NewItems[0] as MyServicesModel;
                Console.WriteLine(e.NewItems[0]);
                Console.WriteLine(e.NewItems[0].GetType());
                if (services.AccessToken == accessToken)
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




    }

 }

