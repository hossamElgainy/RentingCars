using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstraction;
using Core.DomainModels;

namespace Core.Dtos.CarDtos
{
    public class GetAllCarsDto : BaseEntityGuid
    {
        public string ModelName { get; set; }
        public string ModelType { get; set; }
        public string ModelYear { get; set; }
        public string Brand { get; set; }
        public int Power { get; set; }
        public int TotalCount { get; set; }
        public int AvailableCount { get; set; }
    }
}

