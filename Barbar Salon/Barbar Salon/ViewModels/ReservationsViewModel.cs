using Barbar_Salon.Models;
using Barbar_Salon.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Barbar_Salon.ViewModels
{
    public class ReservationsViewModel:BaseViewModel
    {
        private HaloHairServices _firebase;

        private ObservableCollection<ReservationsModel> _reservations;
        private ObservableCollection<ReservationsModel> _filltedReservations;
        private static string _accessToken { get; set; }

        public ICommand DeleteButton { get; }


        public ObservableCollection<ReservationsModel> FilltedReservations
        {
            get
            {
                return _filltedReservations;
            }
            set
            {
                _filltedReservations = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ReservationsModel> Reservations 
        { 
            get { return _reservations; }
            set { _reservations = value;
                OnPropertyChanged();
            
            }
        }

        public ReservationsViewModel()
        {
            AccessToken();
            _firebase = new HaloHairServices();
            Reservations = new ObservableCollection<ReservationsModel>();
            FilltedReservations = new ObservableCollection<ReservationsModel>();

            Reservations = _firebase.GetReservation();

            Reservations.CollectionChanged += ReservationsChanged;

            DeleteButton = new Command(OnDeleteTappend);
            
            

        }

        private async void  AccessToken()
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


        private void ReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
           if(e.Action== NotifyCollectionChangedAction.Add)
            {
               
                    ReservationsModel reservation = e.NewItems[0] as ReservationsModel;

                    if (reservation.BarberAccessToken == _accessToken)
                    {
                       FilltedReservations.Add(reservation);
                    }
            }
            else if(e.Action == NotifyCollectionChangedAction.Remove)
            {
                ReservationsModel reservation = e.OldItems[0] as ReservationsModel;
                FilltedReservations.Remove(reservation);
                
            }
        }

        private async void  OnDeleteTappend(object obj)
        {
            var control = obj as ReservationsModel;
             await _firebase.DeleteReservations(control);

        }

    }
}
