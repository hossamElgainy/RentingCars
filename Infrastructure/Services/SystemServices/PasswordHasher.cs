using Core.Interfaces.IServices.SystemIServices;

namespace Infrastructure.Services.SystemServices
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        public bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var hoso = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
            return hoso;
        }

    }
}
