

using Core.DomainModels;
using Core.Dtos;
using Core.Dtos.CarDtos;
using Core.Dtos.Logs;
using Core.Interfaces.IServices.UsersIServices;
using Core.Interfaces.Specification;
using Core.Interfaces.Specification.SpecParams;
using Core.Interfaces.Specifications;
using Hangfire;
using Infrastructure.Data.Specification;
using System.Data;


namespace Infrastructure.Services.UsersServices
{
    public class BookingService(IUnitOfWork uow) : IBookingService
    {
        public async Task<ServiceResponseDto<string>> AddNewBooking(CreateBookingDto booking)
        {
            if (booking.Id != null)
                return new ServiceResponseDto<string>("Booking Id should be null", false);
            if(booking.From > booking.To)
                return new ServiceResponseDto<string>("From date should be less than To date", false);
            var newBooking = new Booking
            {
                BookedAt = DateTime.Now,
                CustomerName = booking.CustomerName,
                CustomerNationality = booking.CustomerNationality,
                CustomerDrivingLicense = booking.CustomerDrivingLicense,
                Payment = booking.Payment,
                From = booking.From,
                To = booking.To,
            };
            var carsToUpdate = new List<Car>();
            foreach (var car in booking.Cars)
            {
                var carInDb = await uow.Repository<Car>().GetByIdAsync(car.CarId);
                if (carInDb == null)
                    return new ServiceResponseDto<string>("Car not found", false);
                if (carInDb.AvailableCount < car.Quentity)
                    return new ServiceResponseDto<string>("Not enough cars available For This Brand available", false);
                var carBooking = new CarBooking
                {
                    CarId = car.CarId,
                    Quentity = car.Quentity,
                };
                carInDb.AvailableCount-=carBooking.Quentity;
                carsToUpdate.Add(carInDb);
                newBooking.CarBookings.Add(carBooking);
            }
            try
            {
                await uow.Repository<Booking>().AddAsync(newBooking);
                uow.Repository<Car>().UpdateRangeAsync(carsToUpdate);
                await uow.Complete();
                var backgroundJobClient = new BackgroundJobClient();
                backgroundJobClient.Schedule<BookingCompletionService>(
                    x => x.UpdateCarAvailability(newBooking.Id),
                    newBooking.To);
                return new ServiceResponseDto<string>("Booking added successfully", true);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                return new ServiceResponseDto<string>(ex.Message, false);
            }
        }

        public async Task<int> BookingCount()
        {
            return await uow.Repository<Booking>().CountAsync();
        }

        public async Task<ServiceResponseDto<string>> EditBooking(CreateBookingDto bookingDto)
        {
            var booking = await uow.Repository<Booking>().GetByIdAsync(new GetBookingByIdSpec(bookingDto.Id.Value));
            if (booking is null)
                return new ServiceResponseDto<string>("Booking Is Not Exist", false);
            if (booking.From > booking.To)
                return new ServiceResponseDto<string>("From date should be less than To date", false);
            booking.BookedAt = DateTime.Now;
            booking.CustomerName = booking.CustomerName;
            booking.CustomerNationality = booking.CustomerNationality;
            booking.CustomerDrivingLicense = booking.CustomerDrivingLicense;
            booking.Payment = booking.Payment;
            booking.From = booking.From;
            booking.To = booking.To;
            
            foreach (var car in bookingDto.Cars)
            {
                var carInDb = await uow.Repository<Car>().GetByIdAsync(car.CarId);
                if (carInDb == null)
                    return new ServiceResponseDto<string>("Car not found", false);
                var carBooking = await uow.Repository<CarBooking>().GetByIdAsync(car.Id.Value);
                if (carInDb.AvailableCount < car.Quentity)
                    return new ServiceResponseDto<string>("Not enough cars available For This Brand available", false);

                if (carBooking != null)
                {
                    carBooking.CarId = car.CarId;
                    carBooking.Quentity = car.Quentity;
                }
                else
                {
                    var NewcarBooking = new CarBooking
                    {
                        CarId = car.CarId,
                        Quentity = car.Quentity,
                    };
                    booking.CarBookings.Add(NewcarBooking);
                }
              
            }
            try
            {
                await uow.Complete();
                return new ServiceResponseDto<string>("Booking Updated successfully", true);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                return new ServiceResponseDto<string>(ex.Message, false);
            }
        }

        public async Task<PagedResult<GetAllBookingDto>> GetAll(AllBookingSpecParam specParam)
        {

            var bookibg = await uow.Repository<Booking>().GetAllWithSpecAsync(new GetAllBookingSpec(specParam));
            var mappedbooking = bookibg.Select(b => new GetAllBookingDto
            {
                Id = b.Id,
                BookedAt = b.BookedAt,
                CustomerName = b.CustomerName,
                CustomerNationality = b.CustomerNationality,
                CustomerDrivingLicense = b.CustomerDrivingLicense,
                Payment = b.Payment,
                From = b.From,
                To = b.To,
                CarBookings = b.CarBookings.Select(cb => new CarBookingDto
                {
                    Id = cb.Id,
                    CarId = cb.CarId,
                    CarName =  cb.Car.ModelName,
                    Quentity = cb.Quentity,
                }).ToList()
            }).ToList();
            return new PagedResult<GetAllBookingDto>(mappedbooking, await BookingCount(), specParam.PageIndex, specParam.PageSize);

        }

        
    }
}
