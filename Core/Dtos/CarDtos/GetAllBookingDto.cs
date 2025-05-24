using Core.Abstraction;

namespace Core.Dtos.CarDtos
{
    public class GetAllBookingDto : BaseEntityGuid
    {
        public DateTime BookedAt { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNationality { get; set; }
        public string CustomerDrivingLicense { get; set; }
        public string Payment { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public ICollection<CarBookingDto> CarBookings { get; set; } = new HashSet<CarBookingDto>();
    }
}

