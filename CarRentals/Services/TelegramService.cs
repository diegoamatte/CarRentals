using CarRentals.Models;

namespace CarRentals.Services
{
    public class TelegramService : INotificationService<PrivateMessage>
    {
        private ILogger<TelegramService> _logger;

        public TelegramService(ILogger<TelegramService> logger)
        {
            _logger = logger;
        }

        public void Send()
        {
            _logger.LogInformation("Telegram message send");
        }
    }
}
