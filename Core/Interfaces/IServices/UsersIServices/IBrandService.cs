
using Core.Dtos;
using Core.Dtos.CarDtos;

namespace Core.Interfaces.IServices.UsersIServices
{
    public interface IBrandService
    {
        Task<ServiceResponseDto<List<GetAllBrandsDto>>> GetAll();

    }
}
