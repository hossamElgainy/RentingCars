using Core.Dtos.CarDtos;
using Core.Dtos.Logs;
using Core.Interfaces.IServices.UsersIServices;
using Core.Interfaces.Specification.SpecParams;
using Microsoft.AspNetCore.Mvc;

namespace APIS.Controllers
{
    public class CarsController(ICarService car):ApiBaseController
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult<PagedResult<GetAllCarsDto>>> GetAll([FromQuery] AllCarsSpecParam specParam)
        {
            var cars = await car.GetAll(specParam);
            return Ok(cars);
        }
    }
}
