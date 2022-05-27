using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CookBook.Application.Entities.Users
{
  public class AuthOptions
  {
    private const string _key = "ClientCookBookServerPrivateKey";

    public const string Issuer = "CookBookClient";
    public const string Audience = "CookBookServer";
    public const int LifeTimeInMinutes = 2;
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
      return new SymmetricSecurityKey( Encoding.ASCII.GetBytes( _key ) );
    }
  }
}
