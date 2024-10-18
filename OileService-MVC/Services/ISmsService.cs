
namespace OileService_MVC.Services
{
    public interface ISmsService
    {
        Task SendAsync(string phoneNumber, string message);
    }
}