using Solution1.Core.Interfaces.Specification.SpecParams;

namespace Core.Interfaces.Specification.SpecParams
{
    public class AllCarsSpecParam:BasicPaginationParam
    {
        public string? ModelName { get; set; }
        public Guid? BrandId { get; set; }
        public string? ModelYear { get; set; }
    }
}
