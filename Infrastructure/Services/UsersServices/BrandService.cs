

using Core.DomainModels;
using Core.Dtos;
using Core.Dtos.CarDtos;
using Core.Interfaces.IServices.UsersIServices;
using Core.Interfaces.Specifications;
using System.Data;


namespace Infrastructure.Services.UsersServices
{
    public class BrandService(IUnitOfWork uow) : IBrandService
    {
        public async Task<ServiceResponseDto<List<GetAllBrandsDto>>> GetAll()
        {
            var brands = await uow.Repository<Brand>().GetAllAsync();
            var mappedBrands = brands.Select(z=>new GetAllBrandsDto
            {
                Id = z.Id,
                Name = z.Name,
            }).ToList();
            return new ServiceResponseDto<List<GetAllBrandsDto>>(mappedBrands);
        }
    }
}
