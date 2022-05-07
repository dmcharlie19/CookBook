using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CookBook.Application.Entities.Users
{
  public class AuthOptions
  {
    private const string KEY = "CookBookClientCookBookServer";

    public const string ISSUER = "CookBookClient";
    public const string AUDIENCE = "CookBookServer";
    public const int LifeTimeInMinutes = 2;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
      return new SymmetricSecurityKey( Encoding.ASCII.GetBytes( KEY ) );
    }
  }
}
