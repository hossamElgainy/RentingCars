using Solution1.Core.Interfaces.Specification.SpecParams;

namespace Core.Interfaces.Specification.SpecParams
{
    public class AllBookingSpecParam:BasicPaginationParam
    {
        public string? ModelName { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public DateTime? BookedAt { get; set; }


    }
}
