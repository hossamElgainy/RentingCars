

using Core.DomainModels;
using Core.Interfaces.Specification.SpecParams;
using Infrastructure.Data.Specification.Base;


namespace Infrastructure.Data.Specification
{
    public class GetAllBookingSpec : BaseSpecification<Booking>
    {
        public GetAllBookingSpec(AllBookingSpecParam specParam)
        {
            if (!string.IsNullOrEmpty(specParam.CustomerName))
            {
                Criteria.Add(x => x.CustomerName.ToLower().Contains(specParam.CustomerName.ToLower()));
            }
            if (!string.IsNullOrEmpty(specParam.ModelName))
            {
                Criteria.Add(x => x.CarBookings.FirstOrDefault()!.Car.ModelName.ToLower().Contains(specParam.ModelName.ToLower()));
            }
            if (specParam.From.HasValue)
            {
                Criteria.Add(x => x.From.Date >= specParam.From.Value.Date);
            }
            if (specParam.To.HasValue)
            {
                Criteria.Add(x => x.To.Date <= specParam.To.Value.Date);
            }
            if (specParam.BookedAt.HasValue)
            {
                Criteria.Add(x => x.BookedAt.Date == specParam.BookedAt.Value.Date);
            }
            Includes.Add(x => x.CarBookings);
            IncludeStrings.Add("CarBookings.Car");
            OrderByDesc = x => x.BookedAt;
            ApplyPagination(specParam.PageIndex, specParam.PageSize);

        }
    }
}
