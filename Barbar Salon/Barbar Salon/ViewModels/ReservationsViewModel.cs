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
        HaloHairServices firebase;

        private ObservableCollection<ReservationsModel> reservations;
        private ObservableCollection<ReservationsModel> filltedReservations;
        public ObservableCollection<ReservationsModel> FilltedReservations
        {
            get
            {
                return filltedReservations;
            }
            set
            {
                filltedReservations = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ReservationsModel> Reservations 
        { 
            get { return reservations; }
            set { reservations = value;
                OnPropertyChanged();
            
            }
        }

      

        public ICommand DeleteCommand { get; }
        
    

        public ReservationsViewModel()
        {
            AccessToken();
            firebase = new HaloHairServices();
            Reservations = new ObservableCollection<ReservationsModel>();
            FilltedReservations = new ObservableCollection<ReservationsModel>();

            Reservations = firebase.getReservations();

            Reservations.CollectionChanged += reservationschanged;

            DeleteCommand = new Command(OnDeleteTappend);
            
            

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


        private void reservationschanged(object sender, NotifyCollectionChangedEventArgs e)
        {
           if(e.Action== NotifyCollectionChangedAction.Add)
            {
               
                    ReservationsModel reservation = e.NewItems[0] as ReservationsModel;

                    if (reservation.AccessToken_Barbar == accessToken)
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

             await firebase.DeleteReservations(control);

        }

    }
}
