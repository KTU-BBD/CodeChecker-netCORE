using System.Threading.Tasks;
using CodeChecker.Services.EmailSending;

namespace CodeChecker.Tasks
{
    public class SendEmailTask
    {
        private EmailSenderService _emailSenderService;

        public SendEmailTask(EmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        /// <summary>
        /// Sends emails
        /// All email templates needs to stored in ~/Views/Email/{template-name} directory
        ///
        /// Emails need to contain html.cshtml and text.cshtml files
        /// </summary>
        /// <param name="email">Receiver email adress</param>
        /// <param name="subject">Subject of email</param>
        /// <param name="template">Template name</param>
        /// <param name="model">Data which will be passed to email</param>
        /// <typeparam name="TModel"></typeparam>
        public async void Run<TModel>(string email, string subject, string template, TModel model)
        {
            await RunTask(email, subject, template, model);
        }

        private async Task RunTask<TModel>(string email, string subject, string template, TModel model)
        {
            await _emailSenderService.SendEmailAsync(email, subject, template, model);
        }

    }
}