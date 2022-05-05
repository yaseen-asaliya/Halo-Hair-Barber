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
        private HaloHairServices _firebase;
        public ICommand AcceptButton { get; }
        public ICommand RefusedButton { get; }
        private static string _accessToken { get; set; }


        private ObservableCollection<ReservationsRequestModel> _filltedReservationsRequest;

        private ObservableCollection<ReservationsRequestModel> _reservationsRequest;
        public ObservableCollection<ReservationsRequestModel> FilltedReservationsRequest
        {
            get
            {
                return _filltedReservationsRequest;
            }
            set
            {
                _filltedReservationsRequest = value;
                OnPropertyChanged();
            }
        }



        public ObservableCollection<ReservationsRequestModel> ReservationsRequest
        {
            get { return _reservationsRequest; }
            set
            {
                _reservationsRequest = value;
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

        public HomePageViewModel()
        {
            AccessToken();
            _firebase = new HaloHairServices();
            ReservationsRequest = new ObservableCollection<ReservationsRequestModel>();
            FilltedReservationsRequest = new ObservableCollection<ReservationsRequestModel>();
            ReservationsRequest = _firebase.GetReservationsRequest();
            ReservationsRequest.CollectionChanged += ReservationsChanged;
            AcceptButton = new Command(OnAccept);
            RefusedButton = new Command(OnRefused);

   
        }
        private void ReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ReservationsRequestModel reservationRequest = e.NewItems[0] as ReservationsRequestModel;
                if (reservationRequest.BarberAccessToken == _accessToken)
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


        private async void OnAccept(object obj)
        {
            var control = obj as ReservationsRequestModel;
            await _firebase.AcceptReservations(control);

        }
        private async void OnRefused(object obj)
        {
            var control = obj as ReservationsRequestModel;
            await _firebase.RefusedReservations(control);

        }

    }
}
