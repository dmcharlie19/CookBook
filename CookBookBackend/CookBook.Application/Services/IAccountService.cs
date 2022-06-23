using CookBook.Application.Dto;

namespace CookBook.Application.Services
{
    public interface IAccountService
    {
        void AddFavorite( int userId, int recipeId );
        void AddLike( int userId, int recipeId );
        AuthenticationResponseDto Authenticate( AuthenticationRequestDto authRequestDto );
        void Registrate( RegistarationRequestDto registarationDto );
    }
}