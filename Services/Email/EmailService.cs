using Project.Common.DI;

namespace Project.Services.Email;

public class EmailService : IEmailService, IDependencyService
{
  private readonly ILogger<EmailService> _logger;
  private readonly IEmailSender _emailSender;

  public EmailService(ILogger<EmailService> logger, IEmailSender emailSender)
  {
    _logger = logger;
    _emailSender = emailSender;
  }

  public async Task SendAsync()
  {
    try
    {
      // await _emailSender.SendEmailAsync();
      await Task.CompletedTask;
    }
    catch (System.Exception)
    {

      throw;
    }
  }
}