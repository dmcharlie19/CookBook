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
        public List<TagRecipe> Tags { get; set; }

        [Required]
        public List<RecipeStep> RecipeSteps { get; protected set; }

        [Required]
        public List<RecipeIngredient> RecipeIngredients { get; protected set; }

        private const int _maxStepsCount = 50;
        private const int _maxIngredientsCount = 10;
        private const int _maxTagsCount = 4;

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
                throw new InvalidClientParameterException( "���� ������������� �� ������ ���� �������" );

            if ( steps.Count >= _maxStepsCount )
                throw new InvalidClientParameterException( "��������� ������������ ���������� ����� �������������" );

            RecipeSteps = steps;
        }

        public void AddRecipeIngredients( List<RecipeIngredient> ingredients )
        {
            if ( ingredients == null ||
                ingredients.Count == 0 )
                throw new InvalidClientParameterException( "���������� ������� �� ������ ���� �������" );

            if ( ingredients.Count >= _maxIngredientsCount )
                throw new InvalidClientParameterException( "��������� ������������ ���������� ������������" );

            RecipeIngredients = ingredients;
        }

        public void AddTags( List<Tag> tags )
        {
            if ( tags == null ||
                tags.Count == 0 )
                throw new InvalidClientParameterException( "���� �� ������ ���� �������" );

            if ( tags.Count >= _maxTagsCount )
                throw new InvalidClientParameterException( "��������� ������������ ���������� �����" );

            Tags = new();
            foreach ( Tag tag in tags )
            {
                Tags.Add( new TagRecipe( tag.Id ) );
            }
        }
    }
}
