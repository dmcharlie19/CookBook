namespace CookBook.Api.Dto
{
  public class AuthenticationResponseDto
  {
    public string? AccesToken { get; set; }
    public string? UserName { get; set; }
    public int ExpiresAt { get; set; }
  }
}
