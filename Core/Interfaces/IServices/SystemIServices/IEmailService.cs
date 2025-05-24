namespace Core.Interfaces.IServices.SystemIServices
{
    public interface IEmailService
    {

        Task SendEmailAsync(List<string> to, string subject, string html, string from = null);

    }
}
