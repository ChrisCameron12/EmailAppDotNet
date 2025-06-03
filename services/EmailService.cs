using SendGrid;                                
using SendGrid.Helpers.Mail;                
using Microsoft.Extensions.Configuration;     

public class EmailService
{
    private readonly string _apiKey;// Store SendGrid API key

    // SendGrid API key from configuration (e.g., appsettings.json)
    public EmailService(IConfiguration configuration)
    {
        _apiKey = configuration["SendGrid:ApiKey"];
    }


    //Sends an email using SendGrid.
   
    public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
    {
        // Initialize SendGrid client with the API key
        var client = new SendGridClient(_apiKey);

        // Create sender and recipient email addresses
        var from = new EmailAddress("test@gmail.com@gmail.com", "Chris's app"); // Replace with your verified sender
        var to = new EmailAddress(toEmail);

        // Build the email message. Only HTML content is provided; no plain text fallback
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlContent);

        // Send the email
        var response = await client.SendEmailAsync(msg);

        // Handle errors
        if ((int)response.StatusCode >= 400)
        {
            var body = await response.Body.ReadAsStringAsync();
            throw new Exception($"SendGrid error: {response.StatusCode} - {body}");
        }
    }
}
