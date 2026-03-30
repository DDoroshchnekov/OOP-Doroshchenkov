namespace lab31v1
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}