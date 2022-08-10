using Project.Entity.Dto.Email;

namespace Project.Services.Email;

public interface IEmailService
{
  Task SendAsync(UseEmailDto useEmailDto);
}