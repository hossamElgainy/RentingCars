

using Core.DomainModels;
using Core.Dtos.CarDtos;
using Core.Dtos.Logs;
using Core.Interfaces.IServices.UsersIServices;
using Core.Interfaces.Specification.SpecParams;
using Core.Interfaces.Specifications;
using Infrastructure.Data.Specification;
using System.Data;


namespace Infrastructure.Services.UsersServices
{
    public class CarService(IUnitOfWork uow) : ICarService
    {
        public async Task<int> CarsCount()
        {
            return await uow.Repository<Car>().CountAsync();
        }

        public async Task<PagedResult<GetAllCarsDto>> GetAll(AllCarsSpecParam specParam)
        {

            var cars = await uow.Repository<Car>().GetAllWithSpecAsync(new GetAllCarsSpec(specParam));
            var mappedCars = cars.Select(car => new GetAllCarsDto
            {
                Id = car.Id,
                ModelName = car.ModelName,
                ModelYear = car.ModelYear,
                Brand = car.Brand.Name,
                Power = car.Power,
                ModelType = car.ModelType,
                TotalCount = car.TotalCount,
                AvailableCount = car.AvailableCount
            }).ToList();
            return new PagedResult<GetAllCarsDto>(mappedCars, await CarsCount(), specParam.PageIndex, specParam.PageSize);

        }
    }
}
