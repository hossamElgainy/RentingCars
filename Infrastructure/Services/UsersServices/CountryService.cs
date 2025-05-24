using Core.Dtos;
using Core.Interfaces.IServices.UsersIServices;
using CountryData.Standard;


namespace Infrastructure.Services.UsersServices
{
    public class CountryService : ICountryService
    {
        public ServiceResponseDto<List<string>> GetAll()
        {
            var helper = new CountryHelper();
            var countries = helper.GetCountryData()
                .Select(z => $"{z.CountryFlag} {z.CountryName}")
                .ToList();

            return new ServiceResponseDto<List<string>>(countries);
        }
    }
}
