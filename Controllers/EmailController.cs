using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/email")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }

    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
    {
        try
        {
            await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.HtmlContent);
            return Ok("Email sent successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}
