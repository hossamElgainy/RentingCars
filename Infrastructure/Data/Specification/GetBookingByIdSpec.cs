

using Core.DomainModels;
using Infrastructure.Data.Specification.Base;


namespace Infrastructure.Data.Specification
{
    public class GetBookingByIdSpec : BaseSpecification<Booking>
    {
        public GetBookingByIdSpec(Guid id)
        {

            Criteria.Add(x => x.Id == id);

            Includes.Add(x => x.CarBookings);
            IncludeStrings.Add("CarBookings.Car");
        }
    }
}
