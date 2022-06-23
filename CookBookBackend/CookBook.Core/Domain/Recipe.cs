using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CookBook.Core.Exceptions;

namespace CookBook.Core.Domain
{
    public class Recipe
    {
        public int Id { get; protected set; }

        [Required]
        [StringLength( 120, MinimumLength = 3 )]
        public string Title { get; protected set; }

        [Required]
        [StringLength( 200, MinimumLength = 3 )]
        public string ShortDescription { get; protected set; }

        [Required]
        [Range( 1, 1000 )]
        public int PreparingTime { get; protected set; }

        [Required]
        [Range( 1, 1000 )]
        public int PersonCount { get; set; }

        [Required]
        public string ImagePath { get; protected set; }

        [Required]
        public List<TagRecipe> Tags { get; set; }

        [Required]
        public List<RecipeStep> RecipeSteps { get; protected set; }

        [Required]
        public int UserId { get; protected set; }

        public User User { get; protected set; }

        [Required]
        public List<RecipeIngredient> RecipeIngredients { get; protected set; }

        private const int _maxStepsCount = 50;
        private const int _maxIngredientsCount = 10;
        private const int _maxTagsCount = 4;

        public Recipe(
            string title,
            string shortDescription,
            int preparingTime,
            int personCount,
            int userId,
            string imagePath )
        {
            Title = title;
            ShortDescription = shortDescription;
            PreparingTime = preparingTime;
            PersonCount = personCount;
            UserId = userId;
            ImagePath = imagePath;
        }

        public void AddRecipeSteps( List<RecipeStep> steps )
        {
            if ( steps == null ||
                steps.Count == 0 )
                throw new InvalidClientParameterException( "Шаги приготовления не должны быть пустыми" );

            if ( steps.Count >= _maxStepsCount )
                throw new InvalidClientParameterException( "Превышено максимальное количество шагов приготовления" );

            foreach ( var step in steps )
                step.Validate();

            RecipeSteps = steps;
        }

        public void AddRecipeIngredients( List<RecipeIngredient> ingredients )
        {
            if ( ingredients == null ||
                ingredients.Count == 0 )
                throw new InvalidClientParameterException( "Ингриденты рецепта не должны быть пустыми" );

            if ( ingredients.Count >= _maxIngredientsCount )
                throw new InvalidClientParameterException( "Превышено максимальное количество ингредиентов" );

            foreach ( var ingredient in ingredients )
                ingredient.Validate();

            RecipeIngredients = ingredients;
        }

        public void AddTags( List<Tag> tags )
        {
            if ( tags == null ||
                tags.Count == 0 )
                throw new InvalidClientParameterException( "тэги не должны быть пустыми" );

            if ( tags.Count >= _maxTagsCount )
                throw new InvalidClientParameterException( "Превышено максимальное количество тэгов" );

            Tags = new();
            foreach ( Tag tag in tags )
            {
                Tags.Add( new TagRecipe( tag.Id ) );
            }
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
