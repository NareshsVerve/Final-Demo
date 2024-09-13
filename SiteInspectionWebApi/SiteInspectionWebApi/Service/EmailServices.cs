using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace SiteInspectionWebApi.Service
{
    public class EmailServices
    {
        private readonly ILogger<EmailServices> _logger;
        public EmailServices(ILogger<EmailServices> logger) 
        {
            _logger = logger;
        }
        public async Task SendOtpEmailAsync(string email, string otpCode)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Site Inspection App", "nareshsipu2001@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Your OTP Code";

            message.Body = new TextPart("plain")
            {
                Text = $"Your verifcation OTP code is {otpCode}"
            };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("nareshsipu2001@gmail.com", "znso mxuy bczd cplf");

                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Log or handle the error
                _logger.LogError(ex, "An error occured while sending the mail.");
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
