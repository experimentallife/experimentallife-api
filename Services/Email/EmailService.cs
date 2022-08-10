using Microsoft.Extensions.Options;

using Project.Common.DI;
using Project.Entity.Core.Email;
using Project.Entity.Dto.Email;
using Project.Entity.System;

namespace Project.Services.Email;

public class EmailService : IEmailService, IDependencyService
{
  private readonly ILogger<EmailService> _logger;
  private readonly IEmailSender _emailSender;
  private readonly Settings _settings;

  public EmailService(ILogger<EmailService> logger, IEmailSender emailSender, IOptions<Settings> settings)
  {
    _logger = logger;
    _emailSender = emailSender;
    _settings = settings.Value;
  }

  public async Task SendAsync(UseEmailDto useEmailDto)
  {
    try
    {
      var account = _settings.Smtp;

      await _emailSender.SendEmailAsync(
        emailAccount: account,
        subject: "Contact",
        body: useEmailDto.Message,
        fromAddress: useEmailDto.Email,
        fromName: useEmailDto.FullName,
        toAddress: account.Email,
        toName: account.DisplayName
      );
    }
    catch (System.Exception)
    {

      throw;
    }
  }
}