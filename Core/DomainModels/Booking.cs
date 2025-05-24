using Core.Abstraction;

namespace Core.DomainModels
{
    public class Booking : BaseEntityGuid
    {
        public DateTime BookedAt { get; set; } = DateTime.Now;
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNationality { get; set; }
        public string CustomerDrivingLicense { get; set; }
        public string Payment { get; set; }
        public ICollection<CarBooking> CarBookings { get; set; } = new HashSet<CarBooking>();

    }
}