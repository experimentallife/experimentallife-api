using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

using MailKit.Net.Smtp;

using Project.Entity.Core.Email;

namespace Project.Services.Email;

public interface ISmtpBuilder
{
  Task<SmtpClient> BuildAsync(EmailAccount emailAccount = null);

  bool ValidateServerCertificate(
    object sender,
    X509Certificate certificate,
    X509Chain chain,
    SslPolicyErrors sslPolicyErrors
  );
}