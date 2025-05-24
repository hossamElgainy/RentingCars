using Core.Abstraction;

namespace Core.DomainModels
{
    public class CarBooking : BaseEntityGuid
    {
        public int Quentity { get; set; }
        public Guid CarId { get; set; }
        public Car Car { get; set; }
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}