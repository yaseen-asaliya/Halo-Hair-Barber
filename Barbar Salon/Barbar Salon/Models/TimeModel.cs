using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Barbar_Salon.Models
{
    public class TimeModel
    {
        public int Id { get; set; }
        public ObservableCollection<(string, bool)> Time { get; set; }
        public string AccessToken_Barbar { get; set; }
        public string Item1 { get; set; }
        public bool Item2 { get; set; }

    }
}
