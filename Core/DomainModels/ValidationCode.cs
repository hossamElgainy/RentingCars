
using Core.Abstraction;

namespace Core.DomainModels
{
    public class ValidationCode : BaseEntityGuid
    {
        public Guid UserId { get; set; }

        public string Code { get; set; } = null!;

        public DateTime GeneratedDate { get; set; }

        public DateTime ExpirationDate { get; set; }

    }
}
