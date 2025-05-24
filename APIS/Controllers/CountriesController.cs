using Core.Dtos.Response;
using Core.Interfaces.IServices.UsersIServices;
using Microsoft.AspNetCore.Mvc;

namespace APIS.Controllers
{
    public class CountriesController(ICountryService country) : ApiBaseController
    {
        [HttpGet("[action]")]
        public IActionResult Countries()
        {
            var result = country.GetAll();
            if (result.IsSuccess)
            {
                return Ok(new StandardResponse<List<string>>(result.Data));
            }
            else
            {
                return BadRequest(new StandardResponse<string>(result.Message));
            }
        }
    }
}