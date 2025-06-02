using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

public class EmailService
{
    private readonly string _apiKey;

    public EmailService(IConfiguration configuration)
    {
        _apiKey = configuration["SendGrid:ApiKey"];
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("email goes here", "Chris's app");
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent);
        var response = await client.SendEmailAsync(msg);

        if ((int)response.StatusCode >= 400)
        {
            var body = await response.Body.ReadAsStringAsync();
            throw new Exception($"SendGrid error: {response.StatusCode} - {body}");
        }
    }
}
