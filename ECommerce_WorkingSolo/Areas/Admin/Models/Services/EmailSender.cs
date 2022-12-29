using ECommerce_WorkingSolo.Areas.Admin.Models.Options;
using ECommerce_WorkingSolo.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.IO;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using RestSharp;
using RestSharp.Authenticators;

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

    public AuthMessageSenderOptions Options { get; }

    public async Task SendEmailAsync( string toEmail, string subject, string message )
    {
      SendMessageSmtp(Options.MailGunKey, toEmail, subject, message);
    }

    public static void SendMessageSmtp(string mailgunkey,string toEmail, string subject, string message )
    {
      // Compose a message
      MimeMessage mail = new MimeMessage();
      mail.From.Add(new MailboxAddress("Retro Gaming", "admin@retrogaming.com"));
      mail.To.Add(new MailboxAddress("Excited User", toEmail));
      mail.Subject = subject;
      mail.Body = new TextPart("plain")
      {
        Text = message,
      };

      // Send it!
      using (var client = new SmtpClient())
      {
        // XXX - Should this be a little different?
        client.ServerCertificateValidationCallback = ( s, c, h, e ) => true;

        client.Connect("smtp.mailgun.org", 587, false);
        client.AuthenticationMechanisms.Remove("XOAUTH2");
        client.Authenticate("postmaster@sandbox6a11697c0c91439f8336d3c0ce4c319e.mailgun.org", mailgunkey);

        client.Send(mail);
        client.Disconnect(true);
      }
    }


  }
}
