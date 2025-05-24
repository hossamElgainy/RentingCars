using Core.Abstraction;

namespace Core.DomainModels
{
    public class Brand:BaseEntityGuid
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int FoundedYear { get; set; }
        public string LogoUrl { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}
