using Core.Dtos;
using Core.Dtos.CarDtos;
using Core.Dtos.Logs;
using Core.Dtos.Response;
using Core.Interfaces.IServices.UsersIServices;
using Core.Interfaces.Specification.SpecParams;
using Microsoft.AspNetCore.Mvc;

namespace APIS.Controllers
{
    public class BookingController(IBookingService book):ApiBaseController
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult<PagedResult<GetAllBookingDto>>> GetAll([FromQuery] AllBookingSpecParam specParam)
        {
            var booking = await book.GetAll(specParam);
            return Ok(booking);
        }
        [HttpPost("AddOrEditNewBooking")]
        public async Task<ActionResult<ServiceResponseDto<string>>> AddOrNewBooking([FromBody] CreateBookingDto booking)
        {
            if (booking.Id != null)
            {
                var result = await book.EditBooking(booking);
                if (result.IsSuccess)
                    return Ok(new StandardResponse<string>(result.Message));
                else
                    return BadRequest(new StandardResponse<string>(result.Message,false));
            }
            else
            {
                var result = await book.AddNewBooking(booking);
                if (result.IsSuccess)
                    return Ok(new StandardResponse<string>(result.Message));
                else
                    return BadRequest(new StandardResponse<string>(result.Message, false));
            }
        }
    }
}
