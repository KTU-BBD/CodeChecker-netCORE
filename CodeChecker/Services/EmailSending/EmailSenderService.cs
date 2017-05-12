using System.Threading.Tasks;
using CodeChecker.Models.ServiceViewModels;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CodeChecker.Services.EmailSending
{
    public class EmailSenderService
    {
        private readonly MailSettings _settings;
        private readonly ViewRenderService _viewRender;

        public EmailSenderService(IOptions<AppSettings> settings, ViewRenderService viewRender)
        {
            _viewRender = viewRender;
            _settings = settings.Value.MailSettings;
        }

        public async Task SendEmailAsync<TModel>(string email, string subject, string template, TModel model)
        {
            var emailMessage = new MimeMessage();

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = _viewRender.Render(model, template, true),
                TextBody = _viewRender.Render(model, template, false)
            };

            emailMessage.From.Add(new MailboxAddress("test1", "test2@example.com"));
            emailMessage.To.Add(new MailboxAddress("test3", email));
            emailMessage.Subject = subject;
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.LocalDomain = "some.domain.com";
                await client.ConnectAsync(_settings.Address, _settings.Port).ConfigureAwait(false);
                await client.AuthenticateAsync(_settings.UserName, _settings.Password);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}