using Core.DomainModels;
using Infrastructure.Data.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Specification
{
    public class GetValidationCodeSpec:BaseSpecification<ValidationCode>
    {
        public GetValidationCodeSpec(Guid UserId):base(x => x.UserId == UserId)
        {
            OrderByDesc = x => x.GeneratedDate;
        }
    }
}
