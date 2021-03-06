using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using CookBook.Core.Exceptions;

namespace CookBook.Core.Domain
{
    public class TagRecipe
    {
        public int Id { get; protected set; }

        public Recipe Recipe { get; protected set; }
        public int RecipeId { get; protected set; }

        public Tag Tag { get; protected set; }
        public int TagId { get; protected set; }

        public TagRecipe( int tagId )
        {
            TagId = tagId;
        }
    }
}
