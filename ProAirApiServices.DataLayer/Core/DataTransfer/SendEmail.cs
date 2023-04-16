using System.Net;
using System.Net.Mail;

namespace ProAirApiServices.DataLayer.Core.DataTransfer
{
    public static class SendEmail
    {
        #region Fields
        
        private static readonly int SMTP_PORT = 587;
        private static readonly int SMTP_TIMEOUT = 15000;
        private static readonly string PROAIR_RECEIVER = "info@proairfederation.org";
        private static readonly string PROAIR_NO_REPLY = "info@proairfederation.org";
        private static readonly string HOST = "mail.proairfederation.org";
        private static readonly string SMTP_USER = "info@proairfederation.org";
        private static readonly string SMTP_PWD = "BlueCrown25!";
        private static MailMessage message = default!;
        private static SmtpClient smtpClient = new SmtpClient {
            Host = HOST,
            Port = SMTP_PORT,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(SMTP_USER, SMTP_PWD),
            DeliveryMethod = SmtpDeliveryMethod.Network,
            EnableSsl = false,
            Timeout = SMTP_TIMEOUT
        };

        #endregion

        public static void SendToInfo(string subject, string body)
        {
            message = new MailMessage {
                From = new MailAddress(PROAIR_NO_REPLY),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(PROAIR_RECEIVER);

            smtpClient.Send(message);
        }
    }
}
