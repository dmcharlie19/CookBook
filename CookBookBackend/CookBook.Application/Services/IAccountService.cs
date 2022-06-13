using CookBook.Api.Dto;

namespace CookBook.Application.Services
{
  public interface IAccountService
  {
    AuthenticationResponseDto Authenticate( AuthenticationRequestDto authRequestDto );
    void Registrate( RegistarationRequestDto registarationDto );
  }
}