using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using CookBook.Core.Exceptions;

namespace CookBook.Core.Domain
{
    public class Tag
    {
        public int Id { get; protected set; }

        [Required]
        [StringLength( 150, MinimumLength = 3 )]
        public string Name { get; protected set; }

        public Tag( string name )
        {
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
