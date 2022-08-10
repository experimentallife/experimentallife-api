using Newtonsoft.Json;

namespace Project.Entity.Dto.Email;

public class UseEmailDto
{
  [JsonProperty("fullname")]
  public string FullName { get; set; }

  [JsonProperty("email")]
  public string Email { get; set; }

  [JsonProperty("phone")]
  public string Phone { get; set; }

  [JsonProperty("address")]
  public string Address { get; set; }

  [JsonProperty("message")]
  public string Message { get; set; }
}