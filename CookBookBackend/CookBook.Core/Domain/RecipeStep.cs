using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using CookBook.Core.Exceptions;

namespace CookBook.Core.Domain
{
    public class RecipeStep
    {
        public int Id { get; protected set; }

        [Required]
        [StringLength( 1000, MinimumLength = 3 )]
        public string Content { get; protected set; }

        public int RecipeId { get; protected set; }
        public Recipe Recipe { get; protected set; }

        public RecipeStep( string content )
        {
            Content = content;
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
