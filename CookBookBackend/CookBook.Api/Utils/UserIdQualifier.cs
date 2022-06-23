using CookBook.Application.Entities.Users;
using CookBook.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Api.Utils
{
    public class UserIdQualifier
    {
        public static int GetUserId( ControllerBase controller )
        {
            string? userIdString = controller.User.FindFirst( UserClaim.UserId )?.Value;
            if ( userIdString == null )
                throw new InvalidClientParameterException( "user id not found" );

            return int.Parse( userIdString );
        }
    }
}
