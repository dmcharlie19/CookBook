using CookBook.Application.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CookBook.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using CookBook.Application.Repositories;
using CookBook.Core.Domain;
using CookBook.Application.Services;

namespace CookBook.Api.Controllers
{
  [Route( "api/[controller]" )]
  [ApiController]
  [AllowAnonymous]
  public class AccountController : ControllerBase
  {
    private readonly IAccountService _accountService;
    private readonly IUnitOfWork _unitOfWork;
    public AccountController( IUnitOfWork unitOfWork, IAccountService accountService )
    {
      _accountService = accountService;
      _unitOfWork = unitOfWork;
    }

    [HttpPost, Route( "login" )]
    public IActionResult Authentication( [FromBody] AuthenticationRequestDto authRequestDto )
    {
      return Ok( _accountService.Authenticate( authRequestDto ) );
    }

    [HttpPost, Route( "registration" )]
    public IActionResult Registration( [FromBody] RegistarationRequestDto registarationDto )
    {
      _accountService.Registrate( registarationDto );
      _unitOfWork.Commit();

      return Ok();
    }
  }
}
