using CarRentals.DTOs;
using CarRentals.Models;
using CarRentals.Services;
using MediatR;

namespace CarRentals.Commands
{
    public static class SendMessageNotification
    {
        public record SendMessage(RentalDto Rental): INotification;

        public class SendMailNotification : INotificationHandler<SendMessage>
        {
            private INotificationService<Mail> _service;

            public SendMailNotification(INotificationService<Mail> service)
            {
                _service = service;
            }
            public Task Handle(SendMessage notification, CancellationToken cancellationToken)
            {
                _service.Send();
                return Task.CompletedTask;
            }
        }

        public class SendTelegramNotification : INotificationHandler<SendMessage>
        {
            private INotificationService<PrivateMessage> _service;

            public SendTelegramNotification(INotificationService<PrivateMessage> service)
            {
                _service = service;
            }
            public Task Handle(SendMessage notification, CancellationToken cancellationToken)
            {
                _service.Send();
                return Task.CompletedTask;
            }
        }
    }
}
