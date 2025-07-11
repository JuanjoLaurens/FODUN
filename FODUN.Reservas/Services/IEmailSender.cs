using System.Threading.Tasks;

namespace FODUN.Reservas.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
