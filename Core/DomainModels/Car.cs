using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstraction;

namespace Core.DomainModels
{
    public class Car : BaseEntityGuid
    {
        public string ModelName { get; set; }
        public string ModelType { get; set; }
        public string ModelYear { get; set; }
        public Brand Brand { get; set; }
        public Guid BrandId { get; set; }
        public int Power { get; set; }
        public int TotalCount { get; set; }
        public int AvailableCount { get; set; }
        public ICollection<CarBooking> CarBookings { get; set; } = new HashSet<CarBooking>();
    }
}