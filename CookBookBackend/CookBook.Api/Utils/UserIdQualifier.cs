using CookBook.Application.Entities.Users;
using CookBook.Application.Utils;

namespace CookBook.Api.Utils
{
    public class UserIdQualifier : IUserIdQualifier
    {
        private readonly HttpContext? _context;

        public UserIdQualifier( IHttpContextAccessor httpContextAccessor )
        {
            _context = httpContextAccessor.HttpContext;
        }

        public int GetUserId()
        {
            return GetUserId( _context );
        }

        public static int GetUserId( HttpContext? _context )
        {
            if ( _context == null )
                throw new ArgumentNullException( nameof( _context ) );

            string? userIdString = _context.User.FindFirst( UserClaim.UserId )?.Value;

            if ( userIdString == null )
                return 0;

            return int.Parse( userIdString );
        }
    }
}

