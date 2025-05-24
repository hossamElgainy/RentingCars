using Core.Dtos;

namespace Core.Interfaces.IServices.UsersIServices
{
    public interface ICountryService
    {
        ServiceResponseDto<List<string>> GetAll();
    }
}
