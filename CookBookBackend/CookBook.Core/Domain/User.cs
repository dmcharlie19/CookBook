using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CookBook.Core.Exceptions;

namespace CookBook.Core.Domain
{
    public class User
    {
        public int Id { get; protected set; }

        [Required]
        [StringLength( 100, MinimumLength = 5 )]
        public string Login { get; protected set; }

        [Required]
        [StringLength( 100, MinimumLength = 5 )]
        public string Password { get; protected set; }

        [Required]
        [StringLength( 100, MinimumLength = 3 )]
        public string Name { get; protected set; }

        public List<UserFavorite> UserFavorites { get; set; }

        public List<UserLike> UserLikes { get; set; }

        public User(
            string login,
            string password,
            string name )
        {
            Login = login;
            Password = password;
            Name = name;
        }

        public void Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext( this );
            if ( !Validator.TryValidateObject( this, context, results, true ) )
                throw new InvalidClientParameterException( results[ 0 ].ErrorMessage );
        }
    }
}
