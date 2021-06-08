namespace Emailing.Service.API.Interfaces
{
        public interface IMessagingService
        {
                System.Threading.Tasks.Task SendEmail(string to, string subject, string htmlContent);
        }
}