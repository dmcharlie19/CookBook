using CookBook.Application.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CookBook.Api.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace CookBook.Api.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {

        [HttpPost, Route( "login" )]
        public IActionResult Authentication( [FromBody] AuthenticationRequestDto authRequestDto )
        {
            //UserTokenDto userToken = await _accountService.Auth( authRequestDto.Login, authRequestDto.Password );
            //await _unitOfWork.CommitAsync();

            if ( authRequestDto.Login == null )
                return BadRequest();

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
                UserName = "Егор",
                ExpiresAt = ( int )new DateTimeOffset( jwt.ValidTo ).ToUnixTimeSeconds()
            };
            return Ok( response );
        }
    }
}
