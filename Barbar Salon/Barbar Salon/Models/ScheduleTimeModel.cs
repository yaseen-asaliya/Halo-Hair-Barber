using System;
using System.Collections.Generic;
using System.Text;

namespace Barbar_Salon.Models
{
    public class ScheduleTimeModel
    {
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; }
        public string DateSelected { get; set; }
        public string AccessToken_Barbar { get; set; }
        public string NameSalon { get; internal set; }
        public string location { get; internal set; }
        public List<(string, bool)> Time { get; set; }
    }
}
