using System;
using System.Collections.Generic;
using System.Text;

namespace Barbar_Salon.Models
{
    public class ScheduleTimeModel
    {
        public int StartTime { get; set; } 
        public int EndTime { get; set; }
        public string DateSelected { get; set; }
        public string AccessToken_Barbar { get; set; }
        public string NameSalon { get; internal set; }
        public string location { get; internal set; }
    }
}
