namespace CodeChecker.Models.ServiceViewModels
{
    public class MailSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string SenderName { get; set; }
        public string SenderMail { get; set; }
        public int Port { get; set; }
    }
}