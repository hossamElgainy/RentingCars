
using Core.Enums;


namespace Core.Interfaces.IServices.UsersIServices
{
    public interface IUserService
    {

        Task SendVerificationnCode(Guid userId, string userEmail, string Url);
        Task<VerificationCodeValidationResult> ValidateVerificationCode(Guid userId, string verificationCode);
    }
}
