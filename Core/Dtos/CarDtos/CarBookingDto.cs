using Core.Abstraction;

namespace Core.Dtos.CarDtos
{
    public class CarBookingDto:BaseEntityGuid
    {
        public int Quentity { get; set; }
        public string CarName { get; set; }
        public Guid CarId { get; set; }
    }
}

