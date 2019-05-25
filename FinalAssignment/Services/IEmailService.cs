using System.Collections.Generic;
using FinalAssignment.Models.EmailLogic;

namespace FinalAssignment.Services
{
    public interface IEmailService
    {
        void Send(EmailAddress.EmailMessage emailMessage);
        List<EmailAddress.EmailMessage> ReceiveEmail(int maxCount = 10);
    }
}