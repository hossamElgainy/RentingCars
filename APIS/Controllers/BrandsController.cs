using Core.Dtos.CarDtos;
using Core.Dtos.Response;
using Core.Interfaces.IServices.UsersIServices;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Utilities;

namespace APIS.Controllers
{
    public class BrandsController(IBrandService brand):ApiBaseController
    {
        [HttpGet]
        public async Task<ActionResult<StandardResponse<List<GetAllBrandsDto>>>> Get()
        {
            var result = await brand.GetAll();
            if (result.IsSuccess)
                return Ok(new StandardResponse<List<GetAllBrandsDto>>(result.Data));
            else
                return BadRequest(new StandardResponse<string>(result.Message,false));
        }
    }
}
