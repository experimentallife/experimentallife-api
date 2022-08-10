using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using MailKit.Net.Smtp;
using MailKit.Security;

using Project.Common.DI;
using Project.Entity.Core.Email;

namespace Project.Services.Email;

public class SmtpBuilder : ISmtpBuilder, IDependencyService
{
  public SmtpBuilder() { }

  public virtual async Task<SmtpClient> BuildAsync(EmailAccount emailAccount = null)
  {
    if (emailAccount is null)
    {

    }

    var client = new SmtpClient
    {
      ServerCertificateValidationCallback = ValidateServerCertificate
    };

    try
    {
      await client.ConnectAsync(
          emailAccount.Host,
          emailAccount.Port,
          emailAccount.EnableSsl
              ? SecureSocketOptions.SslOnConnect
              : SecureSocketOptions.StartTlsWhenAvailable);

      if (emailAccount.UseDefaultCredentials)
      {
        await client.AuthenticateAsync(CredentialCache.DefaultNetworkCredentials);
      }
      else if (!string.IsNullOrWhiteSpace(emailAccount.Username))
      {
        await client.AuthenticateAsync(new NetworkCredential(emailAccount.Username, emailAccount.Password));
      }

      return client;
    }
    catch (Exception ex)
    {
      client.Dispose();
      throw new Exception(ex.Message, ex);
    }
  }

  public virtual bool ValidateServerCertificate(
    object sender,
    X509Certificate certificate,
    X509Chain chain,
    SslPolicyErrors sslPolicyErrors
  )
  {
    return true;
  }
}