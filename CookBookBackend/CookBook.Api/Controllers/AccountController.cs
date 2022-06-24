using CookBook.Application.Dto;
using CookBook.Application.Repositories;
using CookBook.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
