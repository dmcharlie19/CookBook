using CookBook.Application.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CookBook.Api.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using CookBook.Application.Repositories;
using CookBook.Infrastructure.Repositories;
using CookBookBackend.Core.Domain;
using CookBookBackend.Application.Repositories;

namespace CookBook.Api.Controllers
{
  [Route( "api/[controller]" )]
  [ApiController]
  [AllowAnonymous]
  public class AccountController : ControllerBase
  {
    private IUserRepository _userRepository;
    private IUnitOfWork _unitOfWork;
    public AccountController( IUnitOfWork unitOfWork, IUserRepository userRepository )
    {
      _userRepository = userRepository;
      _unitOfWork = unitOfWork;
    }

    [HttpPost, Route( "login" )]
    public IActionResult Authentication( [FromBody] AuthenticationRequestDto authRequestDto )
    {
      if ( authRequestDto.Login == null )
        return BadRequest();

      var user = _userRepository.Get( authRequestDto.Login );
      if ( user is null )
        return BadRequest( "Пользователь не найден" );

      if ( user.Password != authRequestDto.Password )
        return BadRequest( "Неверный пароль" );

      var claims = new List<Claim> { new Claim( ClaimTypes.Name, authRequestDto.Login ) };
      // создаем JWT-токен
      var jwt = new JwtSecurityToken(
              issuer: AuthOptions.ISSUER,
              audience: AuthOptions.AUDIENCE,
              claims: claims,
              expires: DateTime.UtcNow.Add( TimeSpan.FromMinutes( AuthOptions.LifeTimeInMinutes ) ),
              signingCredentials: new SigningCredentials( AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256 ) );

      var encodedToken = new JwtSecurityTokenHandler().WriteToken( jwt );

      var response = new AuthenticationResponseDto
      {
        AccesToken = encodedToken,
        UserName = user.Name,
        ExpiresAt = ( int )new DateTimeOffset( jwt.ValidTo ).ToUnixTimeSeconds()
      };
      return Ok( response );
    }

    [HttpPost, Route( "registration" )]
    public IActionResult Registration( [FromBody] RegistarationRequestDto registarationDto )
    {
      if ( registarationDto.Login == null ||
           registarationDto.Password == null ||
           registarationDto.Name == null )
        return BadRequest();

      var user = _userRepository.Get( registarationDto.Login );
      if ( user is not null )
        return BadRequest( "Логин занят" );

      _userRepository.Add( new User(
        registarationDto.Login,
        registarationDto.Password,
        registarationDto.Name ) );

      _unitOfWork.Commit();
      return Ok();
    }
  }
}
