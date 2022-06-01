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
        public string? Title { get; protected set; }

        [Required]
        [StringLength( 200, MinimumLength = 3 )]
        public string? ShortDescription { get; protected set; }

        [Required]
        [Range( 1, 1000 )]
        public int PreparingTime { get; protected set; }

        [Required]
        [Range( 1, 1000 )]
        public int PersonCount { get; set; }

        public List<TagRecipe> Tags { get; set; }
        public List<RecipeStep> RecipeSteps { get; protected set; }
        public List<RecipeIngredient> RecipeIngredients { get; protected set; }

        private const int _maxStepsCount = 50;
        private const int _maxIngredientsCount = 10;

        public Recipe(
            string title,
            string shortDescription,
            int preparingTime,
            int personCount
            )
        {
            Title = title;
            ShortDescription = shortDescription;
            PreparingTime = preparingTime;
            PersonCount = personCount;
        }

        public void AddRecipeSteps( List<RecipeStep> steps )
        {
            if ( steps == null ||
                steps.Count == 0 )
                throw new InvalidClientParameterException( "Ўаги приготовлени€ не должны быть пустыми" );

            if ( steps.Count >= _maxStepsCount )
                throw new InvalidClientParameterException( "ѕревышено максимальное количество шагов приготовлени€" );

            RecipeSteps = steps;
        }

        public void AddRecipeIngredients( List<RecipeIngredient> ingredients )
        {
            if ( ingredients == null ||
                ingredients.Count == 0 )
                throw new InvalidClientParameterException( "»нгриденты рецепта не должны быть пустыми" );

            if ( ingredients.Count >= _maxIngredientsCount )
                throw new InvalidClientParameterException( "ѕревышено максимальное количество ингредиентов" );

            RecipeIngredients = ingredients;
        }
    }
}
