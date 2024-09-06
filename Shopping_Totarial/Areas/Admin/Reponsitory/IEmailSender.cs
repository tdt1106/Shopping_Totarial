namespace Shopping_Tutarial.Areas.Admin.Reponsitory
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message); //hàm gửi mail
    }
}
