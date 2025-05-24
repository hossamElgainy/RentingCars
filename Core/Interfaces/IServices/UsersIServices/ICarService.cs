using Core.Dtos.CarDtos;
using Core.Dtos.Logs;
using Core.Interfaces.Specification.SpecParams;

namespace Core.Interfaces.IServices.UsersIServices
{
    public interface ICarService
    {
        Task<PagedResult<GetAllCarsDto>> GetAll(AllCarsSpecParam specParam);
        Task<int> CarsCount();

    }
}
