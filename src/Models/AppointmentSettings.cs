using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntakeQ.ApiClient.Models
{
    public class AppointmentSettings
    {
        public List<BookingService> Services { get; set; }
        public List<BookingLocation> Locations { get; set; }
    }

    public class BookingService
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Duration { get; set; }
    }

    public class BookingLocation
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
