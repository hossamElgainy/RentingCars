

using Core.DomainModels;
using Core.Interfaces.Specifications;
using Infrastructure.Data.Specification;


namespace Infrastructure.Services.UsersServices
{
    public class BookingCompletionService
    {
        private readonly IUnitOfWork _uow;

        public BookingCompletionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task UpdateCarAvailability(Guid bookingId)
        {
            try
            {
                var booking = await _uow.Repository<Booking>().GetByIdAsync(new GetBookingByIdSpec(bookingId));

                if (booking == null) return;

                var carsToUpdate = new List<Car>();

                foreach (var carBooking in booking.CarBookings)
                {
                    var car = await _uow.Repository<Car>().GetByIdAsync(carBooking.CarId);
                    if (car != null)
                    {
                        car.AvailableCount += carBooking.Quentity;
                        carsToUpdate.Add(car);
                    }
                }

                if (carsToUpdate.Any())
                {
                    _uow.Repository<Car>().UpdateRangeAsync(carsToUpdate);
                    await _uow.Complete();
                }
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error updating car availability: {ex.Message}");
            }
        }
    }
}
