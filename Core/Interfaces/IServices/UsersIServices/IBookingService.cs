using Core.Dtos;
using Core.Dtos.CarDtos;
using Core.Dtos.Logs;
using Core.Interfaces.Specification.SpecParams;

namespace Core.Interfaces.IServices.UsersIServices
{
    public interface IBookingService
    {
        Task<PagedResult<GetAllBookingDto>> GetAll(AllBookingSpecParam specParam);
        Task<ServiceResponseDto<string>> AddNewBooking(CreateBookingDto booking);
        Task<ServiceResponseDto<string>> EditBooking(CreateBookingDto booking);
        Task<int> BookingCount();

    }

    public class CreateBookingDto
    {
        public Guid? Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNationality { get; set; }
        public string CustomerDrivingLicense { get; set; }
        public string Payment { get; set; }
        public ICollection<BookingDetailsDto> Cars { get; set; } = new HashSet<BookingDetailsDto>();
    }
    public class BookingDetailsDto
    {
        public Guid? Id { get; set; }
        public Guid CarId { get; set; }
        public int Quentity { get; set; }
    }
}
