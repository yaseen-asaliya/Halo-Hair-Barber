using System;
using System.Collections.Generic;
using System.Text;

namespace Barbar_Salon.Models
{
    public class ScheduleTimeModel
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; }
        public string BarberAccessToken { get; set; }
        public string SalonName { get; internal set; }
        public string Location { get; internal set; }
        public List<(string, bool)> Time { get; set; }
    }
}
