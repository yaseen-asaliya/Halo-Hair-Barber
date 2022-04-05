using System;
using System.Collections.Generic;
using System.Text;
using Barbar_Salon.Services;
using Barbar_Salon.Models;
using System.Collections.ObjectModel;

namespace Barbar_Salon.ViewModels
{
    public class ReservationsViewModel
    {
        FireBaseHaloHair firebase;

        public ObservableCollection<ReservationsModel> Reservations { get; set; }


        public ReservationsViewModel()
        {
            firebase = new FireBaseHaloHair();
            Reservations = new ObservableCollection<ReservationsModel>();
            Reservations = firebase.getReservations();

        }
    }
}
