using CarRentals.Models;

namespace CarRentals.Services
{
    public class MailService : INotificationService<Mail>
    {
        private ILogger<MailService> _logger;

        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;
        }
        public void Send()
        {
            _logger.LogInformation("Mail Send");
        }
    }
}
