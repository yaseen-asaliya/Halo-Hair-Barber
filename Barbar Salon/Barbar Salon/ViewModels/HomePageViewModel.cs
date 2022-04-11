using Barbar_Salon.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Barbar_Salon.Models;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Collections.Specialized;

namespace Barbar_Salon.ViewModels
{
    public class HomePageViewModel:BaseViewModel
    {
        HaloHairServices firebase;

        private ObservableCollection<ReservationsRequestModel> filltedReservationsRequest;
        public ObservableCollection<ReservationsRequestModel> FilltedReservationsRequest
        {
            get
            {
                return filltedReservationsRequest;
            }
            set
            {
                filltedReservationsRequest = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ReservationsRequestModel> reservationsRequest;

        public ObservableCollection<ReservationsRequestModel> ReservationsRequest
        {
            get { return reservationsRequest; }
            set
            {
                reservationsRequest = value;
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

        public HomePageViewModel()
        {
            AccessToken();
            firebase = new HaloHairServices();
            ReservationsRequest = new ObservableCollection<ReservationsRequestModel>();
            FilltedReservationsRequest = new ObservableCollection<ReservationsRequestModel>();

            ReservationsRequest = firebase.getReservationsRequest();

            ReservationsRequest.CollectionChanged += reservationschanged;

            Accept = new Command(OnAccept);
            Refused = new Command(OnRefused);

   
        }



       



        private void reservationschanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ReservationsRequestModel reservationRequest = e.NewItems[0] as ReservationsRequestModel;
                Console.WriteLine( e.NewItems[0]);
                Console.WriteLine(e.NewItems[0].GetType());
                if (reservationRequest.AccessToken == accessToken)
                {

                    FilltedReservationsRequest.Add(reservationRequest);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                ReservationsRequestModel reservationRequest = e.OldItems[0] as ReservationsRequestModel;


                FilltedReservationsRequest.Remove(reservationRequest);

            }


        }













        public ICommand Accept { get; }
        public ICommand Refused { get; }


        private void OnAccept(object obj)
        {
            var control = obj as ReservationsRequestModel;

            firebase.AcceptReservations(control);


        }
        private void OnRefused(object obj)
        {
            var control = obj as ReservationsRequestModel;

            firebase.RefusedReservations(control);


        }

    }
}
