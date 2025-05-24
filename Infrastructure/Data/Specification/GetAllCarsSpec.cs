

using Core.DomainModels;
using Core.Interfaces.Specification.SpecParams;
using Infrastructure.Data.Specification.Base;


namespace Infrastructure.Data.Specification
{
    public class GetAllCarsSpec : BaseSpecification<Car>
    {
        public GetAllCarsSpec(AllCarsSpecParam specParam)
        {
            if (!string.IsNullOrEmpty(specParam.ModelName))
            {
                Criteria.Add(x => x.ModelName.ToLower().Contains(specParam.ModelName.ToLower()));
            }
            if (!string.IsNullOrEmpty(specParam.ModelYear))
            {
                Criteria.Add(x => x.ModelYear.ToLower().Contains(specParam.ModelYear.ToLower()));
            }
            if (specParam.BrandId != null)
            {
                Criteria.Add(x => x.BrandId == specParam.BrandId);
            }
            Includes.Add(z => z.Brand);
            ApplyPagination(specParam.PageIndex, specParam.PageSize);
        }
    }
}
