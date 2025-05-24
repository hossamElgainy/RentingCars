


using Core.DomainModels;

namespace Core.Interfaces.IServices.SystemIServices
{
    public interface ITokenService
    {
        Task<string> CreateTokenForUser(User user);
        DateTime? ExtractValidToDateFromToken(string tokenString);
    }
}
