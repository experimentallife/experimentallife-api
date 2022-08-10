using MimeKit;
using MimeKit.Text;

using Project.Common.DI;
using Project.Entity.Core.Email;

namespace Project.Services.Email;

public class EmailSender : IEmailSender, IDependencyService
{
  private readonly ISmtpBuilder _smtpBuilder;

  public EmailSender(ISmtpBuilder smtpBuilder)
  {
    _smtpBuilder = smtpBuilder;
  }

  protected MimePart CreateMimeAttachment(
    string attachmentFileName,
    byte[] binaryContent,
    DateTime cDate,
    DateTime mDate,
    DateTime rDate
  )
  {
    if (!ContentType.TryParse(MimeTypes.GetMimeType(attachmentFileName), out var mimeContentType))
      mimeContentType = new ContentType("application", "octet-stream");

    return new MimePart(mimeContentType)
    {
      FileName = attachmentFileName,
      Content = new MimeContent(new MemoryStream(binaryContent)),
      ContentDisposition = new ContentDisposition
      {
        CreationDate = cDate,
        ModificationDate = mDate,
        ReadDate = rDate
      }
    };
  }

  public virtual async Task SendEmailAsync(
    EmailAccount emailAccount,
    string subject,
    string body,
    string fromAddress,
    string fromName,
    string toAddress,
    string toName,
    string replyTo = null,
    string replyToName = null,
    IEnumerable<string> bcc = null,
    IEnumerable<string> cc = null,
    string attachmentFilePath = null,
    string attachmentFileName = null,
    int attachedDownloadId = 0,
    IDictionary<string, string> headers = null
  )
  {
    var message = new MimeMessage();

    message.From.Add(new MailboxAddress(fromName, fromAddress));
    message.To.Add(new MailboxAddress(toName, toAddress));

    if (!string.IsNullOrEmpty(replyTo))
    {
      message.ReplyTo.Add(new MailboxAddress(replyToName, replyTo));
    }

    if (bcc != null)
    {
      foreach (var address in bcc.Where(bccValue => !string.IsNullOrWhiteSpace(bccValue)))
      {
        message.Bcc.Add(new MailboxAddress("", address.Trim()));
      }
    }

    if (cc != null)
    {
      foreach (var address in cc.Where(ccValue => !string.IsNullOrWhiteSpace(ccValue)))
      {
        message.Cc.Add(new MailboxAddress("", address.Trim()));
      }
    }

    message.Subject = subject;

    if (headers != null)
      foreach (var header in headers)
      {
        message.Headers.Add(header.Key, header.Value);
      }

    var multipart = new Multipart("mixed")
    {
      new TextPart(TextFormat.Html) { Text = body }
    };
    message.Body = multipart;

    using var smtpClient = await _smtpBuilder.BuildAsync(emailAccount);
    await smtpClient.SendAsync(message);
    await smtpClient.DisconnectAsync(true);
  }
}