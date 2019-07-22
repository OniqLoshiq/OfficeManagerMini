using System.Threading.Tasks;

namespace OMM.Services.SendGrid
{
    public interface ISendGrid
    {
        Task<bool> SendRegistrationMailAsync(string recipientEmail, string recipientName, string password);
    }
}
