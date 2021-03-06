using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CookBook.Api.Dto;
using CookBook.Application.Entities.Users;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Core.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace CookBook.Application.Services
{
    public class AccountService : IAccountService
    {
        private IUserRepository _userRepository;
        public AccountService( IUserRepository userRepository )
        {
            _userRepository = userRepository;
        }
        public AuthenticationResponseDto Authenticate( AuthenticationRequestDto authRequestDto )
        {
            if ( authRequestDto.Login == null )
                throw new InvalidClientParameterException( "Неверный логин" );

            var user = _userRepository.Get( authRequestDto.Login );
            if ( user is null )
                throw new InvalidClientParameterException( "Пользователь не найден" );

            // #Todo шифровка пароля
            if ( user.Password != authRequestDto.Password )
                throw new InvalidClientParameterException( "Неверный пароль" );

            var claims = new List<Claim> { new Claim( ClaimTypes.Name, authRequestDto.Login ),
                                           new Claim( UserClaim.UserId, user.Id.ToString() )};

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.Add( TimeSpan.FromMinutes( AuthOptions.LifeTimeInMinutes ) ),
                    signingCredentials: new SigningCredentials( AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256 ) );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken( jwt );

            var response = new AuthenticationResponseDto
            {
                Id = user.Id,
                AccesToken = encodedToken,
                UserName = user.Name,
                ExpiresAt = jwt.ValidTo
            };
            return response;
        }

        public void Registrate( RegistarationRequestDto registarationDto )
        {
            if ( registarationDto.Login == null ||
                 registarationDto.Password == null ||
                 registarationDto.Name == null )
                throw new InvalidClientParameterException( "Ошибка в данных" );

            var user = _userRepository.Get( registarationDto.Login );
            if ( user is not null )
                throw new InvalidClientParameterException( "Логин занят" );

            user = new User(
              registarationDto.Login,
              registarationDto.Password,
              registarationDto.Name );

            user.Validate();
            _userRepository.Add( user );
        }
    }
}
