using ECommerce_WorkingSolo.Areas.Admin.Models.Options;
using ECommerce_WorkingSolo.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Diagnostics;

namespace ECommerce_WorkingSolo.Areas.Admin.Models.Services
{
  public class EmailSender: IEmailSender
  {
    private readonly ILogger _logger;
    private readonly ECommerceDbContext _context;
    public EmailSender( IOptions<AuthMessageSenderOptions> optionsAccessor, ILogger<EmailSender> logger, ECommerceDbContext context )
    {
      Options = optionsAccessor.Value;
      _logger = logger;
      _context = context;
    }

    public AuthMessageSenderOptions Options { get; } // set with secret manager

    public async Task SendEmailAsync( string toEmail, string subject, string message )
    {
      if (string.IsNullOrEmpty(Options.SendGridKey))
      {
        throw new Exception("Null SendGridKey");
      }
      //await Execute(Options.SendGridKey, subject, message, toEmail);
      Execute().Wait();
    }

    public async Task Execute()
    {
      var apiKey = Options.SendGridKey;
      var client = new SendGridClient(apiKey);
      var from = new EmailAddress("admin@retrogaming.com", "Example User");
      var subJect = "Sending with SendGrid is Fun";
      var to = new EmailAddress("admin@retrogaming.com", "Example User");
      var plainTextContent = "and easy to do anywhere, even with C#";
      var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
      var msg = MailHelper.CreateSingleEmail(from, to, subJect, plainTextContent, htmlContent);
      var response = await client.SendEmailAsync(msg);

      if(client != null)
      {
        await client.SendEmailAsync(msg);
      }
      else
      {
        Trace.TraceError("Failed to create web transport");
        await Task.FromResult(0);
      }
    }

    public async Task Execute( string apiKey, string subject, string message, string toEmail )
    {
      var client = new SendGridClient(apiKey);
      var msg = new SendGridMessage()
      {
        //From = new EmailAddress("admin@retrogaming.com", "Password Recovery"),
        From = new EmailAddress("test@example.com", "Example User"),
        Subject = subject,
        PlainTextContent = message,
        HtmlContent = message
      };
      msg.AddTo(new EmailAddress(toEmail));

      // Disable click tracking.
      // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
      msg.SetClickTracking(false, false);
      var response = await client.SendEmailAsync(msg);
      _logger.LogInformation(response.IsSuccessStatusCode
                             ? $"Email to {toEmail} queued successfully!"
                             : $"Failure Email to {toEmail}");
    }


  }
}
