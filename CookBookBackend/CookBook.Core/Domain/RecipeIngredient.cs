using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CookBook.Core.Exceptions;

namespace CookBook.Core.Domain
{
    public class RecipeIngredient
    {
        public int Id { get; protected set; }

        [Required]
        [StringLength( 100, MinimumLength = 3 )]
        public string Title { get; protected set; }

        [Required]
        [StringLength( 1000, MinimumLength = 3 )]
        public string Content { get; protected set; }

        public int RecipeId { get; protected set; }
        public Recipe Recipe { get; protected set; }

        public RecipeIngredient( string title, string content )
        {
            Title = title;
            Content = content;
        }

        public void Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext( this );
            if ( !Validator.TryValidateObject( this, context, results, true ) )
                throw new InvalidClientParameterException( results[ 0 ].ErrorMessage );
        }

        public string[] GetIngredients()
        {
            return Content.Split( "_" );
        }

        public static string Convert( string[] ingredients )
        {
            if ( ingredients.Where( i => i.Contains( "_" ) ).Count() != 0 )
                throw new InvalidClientParameterException( "Недопустимый символ '_' в ингридиентах" );

            return string.Join( "_", ingredients );
        }
    }

}
