using System.Net;

using Microsoft.AspNetCore.Mvc;

using Project.Controllers.Base;
using Project.Entity.Dto.Email;
using Project.Services.Email;

namespace Project.Controllers;

[ApiController]
[Route("/")]
public class HomeController : BaseApieNoAuthorizeController
{
  private readonly IEmailService _emailService;
  private readonly ILogger<HomeController> _logger;

  public HomeController(ILogger<HomeController> logger, IEmailService emailService)
  {
    _logger = logger;
    _emailService = emailService;
  }

  [HttpGet]
  public HttpResponseMessage Index()
  {
    var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Found);
    httpResponseMessage.Headers.Location = new Uri("/swagger/ui/index", UriKind.Relative);
    return httpResponseMessage;
  }

  [HttpPost]
  [Route("contact")]
  public async Task<ActionResult<object>> Contact(
    [FromBody] UseEmailDto useEmailDto
  )
  {
    await _emailService.SendAsync(useEmailDto);
    return Success();
  }
}
